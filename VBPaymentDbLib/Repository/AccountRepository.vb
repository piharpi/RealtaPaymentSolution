Imports System.ComponentModel
Imports System.Data.SqlClient
Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Model

Namespace Repository
    Public Class AccountRepository
        Implements IAccountRepository

        Private ReadOnly _context As IRepositoryContext

        Sub New(repoContext As IRepositoryContext)
            _context = repoContext
        End Sub
        Public Function FindAll(Optional limit As Integer = 10) As List(Of Account) Implements IAccountRepository.FindAll
            Dim accounts As New List(Of Account)
            Dim query = "SELECT TOP (@limit) [usac_entity_id]
                          ,[usac_user_id]
                          ,[usac_account_number]
                          ,[usac_saldo]
                          ,[usac_type]
                          ,[usac_expmonth]
                          ,[usac_expyear]
                          ,[usac_modified_date]
                         FROM [Payment].[user_accounts]"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@limit", limit)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        While reader.Read()
                            accounts.Add(New Account(
                                      entityId:=reader.GetInt32(0),
                                      userId:=reader.GetInt32(1),
                                      accountNumber:=reader.GetString(2),
                                      saldo:=reader.GetDecimal(3),
                                      type:=reader.GetString(4),
                                      expMonth:=reader.GetByte(5),
                                      expYear:=reader.GetInt16(6),
                                      modifiedDate:=reader.GetDateTime(7)
                            ))
                        End While
                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return accounts
        End Function

        Public Function FindById(entityId As Integer, userId As Integer) As Account Implements IAccountRepository.FindById
            Dim account As New Account() With {.UserId = userId}
            Dim query = "SELECT TOP 1 [usac_entity_id] 
                          ,[usac_account_number]
                          ,[usac_saldo]
                          ,[usac_type]
                          ,[usac_expmonth]
                          ,[usac_expyear]
                          ,[usac_modified_date]
                         FROM [Payment].[user_accounts]
                         WHERE [usac_entity_id] = @entityId AND [usac_user_id] = @userId"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@entityId", entityId)
                    cmd.Parameters.AddWithValue("@userId", userId)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            account.EntityId = reader.GetInt32(0)
                            account.AccountNumber = reader.GetString(1)
                            account.Saldo = reader.GetDecimal(2)
                            account.Type = reader.GetString(3)
                            account.ExpMonth = reader.GetByte(4)
                            account.ExpYear = reader.GetInt16(5)
                            account.ModifiedDate = reader.GetDateTime(6)
                            reader.Close()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return account
        End Function

        Public Function Create(newAccount As Account) As Account Implements IAccountRepository.Create
            Dim account As New Account()

            Dim query = "INSERT INTO [Payment].[user_accounts]
                                    ([usac_entity_id]
                                    ,[usac_user_id]
                                    ,[usac_account_number]
                                    ,[usac_saldo]
                                    ,[usac_type]
                                    ,[usac_expmonth]
                                    ,[usac_expyear]
                                    ,[usac_modified_date])
                         VALUES (@entityId, @userId, @accountNumber, @saldo, @type, @expMonth, @expYear, GETDATE());"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@entityId", newAccount.EntityId)
                    cmd.Parameters.AddWithValue("@userId", newAccount.UserId)
                    cmd.Parameters.AddWithValue("@accountNumber", newAccount.AccountNumber)
                    cmd.Parameters.AddWithValue("@saldo", newAccount.Saldo)
                    cmd.Parameters.AddWithValue("@type", newAccount.Type)
                    cmd.Parameters.AddWithValue("@expMonth", newAccount.ExpMonth)
                    cmd.Parameters.AddWithValue("@expYear", newAccount.ExpYear)

                    Try
                        conn.Open()
                        cmd.ExecuteScalar()
                        Dim entityId = newAccount.EntityId
                        Dim userId = newAccount.UserId
                        account = FindById(entityId, userId)
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return account
        End Function

        'TODO: Pada store procedure buat sebuah transaction dan commit
        Public Function Update(entityId As Integer, userId As Integer,
                               Optional newEntityId As Integer = Nothing,
                               Optional newUserId As Integer = Nothing,
                               Optional accountNumber As String = Nothing,
                               Optional saldo As Decimal = Nothing,
                               Optional type As String = Nothing,
                               Optional expMonth As Byte = Nothing,
                               Optional expYear As Int16 = Nothing) As Account Implements IAccountRepository.Update


            Dim account As New Account()
            Dim defaultValue = FindById(entityId, userId)

            Dim query = "[Payment].[spUpdateUserAccount] @findEntityId, @findUserId, @entityId, @userId, @accountNumber, @saldo, @type, @expMonth, @expYear"

            If newEntityId.Equals(0) Then
                newEntityId = defaultValue.EntityId
            End If

            If newUserId.Equals(0) Then
                newUserId = defaultValue.UserId
            End If

            If accountNumber Is Nothing Then
                accountNumber = defaultValue.AccountNumber
            End If

            If saldo.Equals(0) Then
                saldo = defaultValue.Saldo
            End If

            If type Is Nothing Then
                type = defaultValue.Type
            End If

            If expMonth.Equals(0) Then
                expMonth = defaultValue.ExpMonth
            End If

            If expYear.Equals(0) Then
                expYear = defaultValue.ExpYear
            End If

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@findEntityId", entityId)
                    cmd.Parameters.AddWithValue("@findUserId", userId)
                    cmd.Parameters.AddWithValue("@entityId", newEntityId)
                    cmd.Parameters.AddWithValue("@userId", newUserId)
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber)
                    cmd.Parameters.AddWithValue("@saldo", saldo)
                    cmd.Parameters.AddWithValue("@type", type)
                    cmd.Parameters.AddWithValue("@expMonth", expMonth)
                    cmd.Parameters.AddWithValue("@expYear", expYear)

                    Try
                        conn.Open()
                        If cmd.ExecuteNonQuery() Then
                            account = FindById(newEntityId, newUserId)
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return account
        End Function

        'TODO : pada saat user terhapus maka user_accounts juga akan terhapus
        'TODO : pada saat menghapus user_accounts akan menghapus/trigger beserta entityBank / entityPayment melalui tabel entity
        Public Function Delete(entityId As Integer, userId As Integer) As Integer Implements IAccountRepository.Delete
            Dim deleteRowAffected As Integer
            Dim isExistBank = FindById(entityId, userId)

            Dim sqlQuery = "DELETE FROM [Payment].[user_accounts] 
                             WHERE [usac_entity_id] = @entityId AND [usac_user_id] = @userId"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = sqlQuery}
                    cmd.Parameters.AddWithValue("@entityId", entityId)
                    cmd.Parameters.AddWithValue("@userId", userId)

                    Try
                        conn.Open()
                        If isExistBank.EntityId.Equals(0) Then
                            Console.WriteLine("entityId atau userId tidak ada di user_accounts")
                        Else
                            deleteRowAffected = cmd.ExecuteNonQuery()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return deleteRowAffected
        End Function
    End Class
End Namespace
Imports System.Data.SqlClient
Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Model

Namespace Repository
    Public Class BankRepository
        Implements IBankRepository

        Private ReadOnly _context As IRepositoryContext

        Sub New(repoContext As IRepositoryContext)
            If _context Is Nothing Then
                _context = repoContext
            End If
        End Sub

        Public Function FindAll(Optional limit As Integer = 10) As List(Of Bank) Implements IBankRepository.FindAll
            Dim banks As New List(Of Bank)
            Dim query = "SELECT TOP (@limit) [bank_entity_id]
                          ,[bank_code]
                          ,[bank_name]
                          ,[bank_modified_date]
                         FROM [Payment].[bank]"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@limit", limit)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        While reader.Read()
                            banks.Add(New Bank(
                                      entityId:=reader.GetInt32(0),
                                      code:=reader.GetString(1),
                                      name:=reader.GetString(2),
                                      modifiedDate:=reader.GetDateTime(3)
                            ))
                        End While
                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return banks
        End Function

        Public Function FindById(id As Integer) As Bank Implements IBankRepository.FindById
            Dim bank As New Bank()
            Dim query = "SELECT TOP 1 [bank_entity_id]
                          ,[bank_code]
                          ,[bank_name]
                          ,[bank_modified_date]
                         FROM [Payment].[bank]
                         WHERE [bank_entity_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            bank.EntityId = reader.GetInt32(0)
                            bank.Code = reader.GetString(1)
                            bank.Name = reader.GetString(2)
                            bank.ModifiedDate = reader.GetDateTime(3)
                            reader.Close()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return bank
        End Function

        Public Function Create(newBank As Bank) As Bank Implements IBankRepository.Create
            Dim bank As New Bank()
            Dim query = "INSERT 
                           INTO [Payment].[bank] ([bank_code], [bank_name])
                         VALUES (@code, @name);" &
                         "SELECT CAST(IDENT_CURRENT('Payment.Entity') AS INT);"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@code", newBank.Code)
                    cmd.Parameters.AddWithValue("@name", newBank.Name)

                    Try
                        conn.Open()
                        Dim bankId = Convert.ToInt32(cmd.ExecuteScalar())
                        bank = FindById(bankId)
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return bank
        End Function

        'TODO: Pada store procedure buat sebuah transaction dan commit
        Public Function Update(id As Integer, Optional code As String = Nothing, Optional name As String = Nothing) As Bank Implements IBankRepository.Update
            Dim bank As New Bank()
            Dim defaultValue = FindById(id)

            'Dim query = "UPDATE [Payment].[bank] 
            '                SET [bank_code] = @code,
            '                    [bank_name] = @name,
            '                    [bank_modified_date] = @modifiedDate
            '              WHERE [bank_entity_id] = @id"

            Dim query = "[Payment].[spUpdateBank] @id, @code, @name"

            If code Is Nothing Then
                code = defaultValue.Code
            End If

            If name Is Nothing Then
                name = defaultValue.Name
            End If

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@code", code)
                    cmd.Parameters.AddWithValue("@name", name)

                    Try
                        conn.Open()
                        If cmd.ExecuteNonQuery() Then
                            bank = FindById(id)
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return bank
        End Function

        Public Function Delete(id As Integer) As Integer Implements IBankRepository.Delete
            Dim deleteRowAffected As Integer
            Dim isExistBank = FindById(id)

            Dim query = "DELETE FROM [Payment].[entity] WHERE [entity_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        If isExistBank.EntityId.Equals(0) Then
                            Console.WriteLine("Id tidak ada dibank")
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
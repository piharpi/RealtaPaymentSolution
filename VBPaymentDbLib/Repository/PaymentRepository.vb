Imports System.Data.SqlClient
Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Model

Namespace Repository
    Public Class PaymentRepository
        Implements IPaymentRepository

        Private ReadOnly _context As IRepositoryContext

        Sub New(repoContext As IRepositoryContext)
            If _context Is Nothing Then
                _context = repoContext
            End If
        End Sub

        Public Function FindAll(Optional limit As Integer = 10) As List(Of Payment) Implements IPaymentRepository.FindAll
            Dim payments As New List(Of Payment)
            Dim query = "SELECT TOP (@limit) [paga_entity_id]
                          ,[paga_code]
                          ,[paga_name]
                          ,[paga_modified_date]
                         FROM [Payment].[payment_gateway]"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@limit", limit)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        While reader.Read()
                            payments.Add(New Payment(
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

            Return payments
        End Function

        Public Function FindById(id As Integer) As Payment Implements IPaymentRepository.FindById
            Dim payment As New Payment()
            Dim query = "SELECT TOP 1 [paga_entity_id]
                          ,[paga_code]
                          ,[paga_name]
                          ,[paga_modified_date]
                         FROM [Payment].[payment_gateway]
                         WHERE [paga_entity_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            payment.EntityId = reader.GetInt32(0)
                            payment.Code = reader.GetString(1)
                            payment.Name = reader.GetString(2)
                            payment.ModifiedDate = reader.GetDateTime(3)
                            reader.Close()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return payment
        End Function

        Public Function Create(newPayment As Payment) As Payment Implements IPaymentRepository.Create
            Dim payment As New Payment()
            Dim query = "INSERT 
                           INTO [Payment].[payment_gateway] ([paga_code], [paga_name])
                         VALUES (@code, @name);" &
                         "SELECT CAST(IDENT_CURRENT('Payment.Entity') AS INT);"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@code", newPayment.Code)
                    cmd.Parameters.AddWithValue("@name", newPayment.Name)

                    Try
                        conn.Open()
                        Dim paymentId = Convert.ToInt32(cmd.ExecuteScalar())
                        payment = FindById(paymentId)
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return payment
        End Function

        'TODO: Pada store procedure buat sebuah transaction dan commit
        Public Function Update(id As Integer, Optional code As String = Nothing, Optional name As String = Nothing) As Payment Implements IPaymentRepository.Update
            Dim payment As New Payment()
            Dim defaultValue = FindById(id)

            'Dim query = "UPDATE [Payment].[payment_gateway] 
            '                SET [paga_code] = @code,
            '                    [paga_name] = @name,
            '                    [paga_modified_date] = @modifiedDate
            '              WHERE [paga_entity_id] = @id"
            Dim query = "[Payment].[spUpdatePaymentGateway] @id, @code, @name"

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
                            payment = FindById(id)
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return payment
        End Function

        Public Function Delete(id As Integer) As Integer Implements IPaymentRepository.Delete
            Dim deleteRowAffected As Integer
            Dim isExistPayment = FindById(id)

            Dim sqlQuery = "DELETE FROM [Payment].[entity] WHERE [entity_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = sqlQuery}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        If isExistPayment.EntityId.Equals(0) Then
                            Console.WriteLine("Id tidak ada dipayment")
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
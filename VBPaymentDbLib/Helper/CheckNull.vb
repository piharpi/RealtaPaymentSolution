Namespace Helper
    Module CheckNull
        Public Function isNull(ByVal checkValue As Object, ByVal returnIfNull As Object) As Object
            If checkValue Is DBNull.Value Then
                Return returnIfNull
            Else
                Return checkValue
            End If
        End Function
    End Module
End Namespace
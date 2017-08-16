Imports System.ComponentModel.DataAnnotations

Public Class ContactMetadata

    <Required(ErrorMessage:="First name is required.")>
    <Display(Name:="First Name")>
    Public Property FirstName As String

    <Display(Name:="Last Name")>
    Public Property LastName As String

    <StringLength(10, ErrorMessage:="No more than 10 numbers.")>
    Public Property Phone As String

    <DataType(DataType.EmailAddress)>
    Public Property Email As String

End Class

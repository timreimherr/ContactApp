Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports ContactApp

Namespace Controllers.Api
    Public Class ContactsController
        Inherits System.Web.Http.ApiController

        Private db As New timdbEntities

        ' GET: api/Contacts
        Function GetContacts() As IQueryable(Of Contact)
            Return db.Contacts
        End Function

        ' GET: api/Contacts/5
        <ResponseType(GetType(Contact))>
        Function GetContact(ByVal id As Integer) As IHttpActionResult
            Dim contact As Contact = db.Contacts.Find(id)
            If IsNothing(contact) Then
                Return NotFound()
            End If

            Return Ok(contact)
        End Function

        ' PUT: api/Contacts/5
        <ResponseType(GetType(Void))>
        Function PutContact(ByVal id As Integer, ByVal contact As Contact) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = contact.Id Then
                Return BadRequest()
            End If

            db.Entry(contact).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (ContactExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Contacts
        <ResponseType(GetType(Contact))>
        Function PostContact(ByVal contact As Contact) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Contacts.Add(contact)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = contact.Id}, contact)
        End Function

        ' DELETE: api/Contacts/5
        <ResponseType(GetType(Contact))>
        Function DeleteContact(ByVal id As Integer) As IHttpActionResult
            Dim contact As Contact = db.Contacts.Find(id)
            If IsNothing(contact) Then
                Return NotFound()
            End If

            db.Contacts.Remove(contact)
            db.SaveChanges()

            Return Ok(contact)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function ContactExists(ByVal id As Integer) As Boolean
            Return db.Contacts.Count(Function(e) e.Id = id) > 0
        End Function
    End Class
End Namespace
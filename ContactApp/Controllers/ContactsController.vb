Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ContactApp
Imports ContactApp.timdbEntities


Namespace Controllers
    Public Class ContactsController
        Inherits System.Web.Mvc.Controller

        Private db As New timdbEntities

        ' GET: Contacts
        Function Index() As ActionResult
            Return View(db.Contacts.ToList())
        End Function

        ' GET: Contacts/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim contact As Contact = db.Contacts.Find(id)
            If IsNothing(contact) Then
                Return HttpNotFound()
            End If
            Return View(contact)
        End Function

        ' GET: Contacts/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Contacts/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,FirstName,LastName,Phone,Email")> ByVal contact As Contact) As ActionResult
            If ModelState.IsValid Then
                db.Contacts.Add(contact)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(contact)
        End Function

        ' GET: Contacts/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim contact As Contact = db.Contacts.Find(id)
            If IsNothing(contact) Then
                Return HttpNotFound()
            End If
            Return View(contact)
        End Function

        ' POST: Contacts/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,FirstName,LastName,Phone,Email")> ByVal contact As Contact) As ActionResult
            If ModelState.IsValid Then
                db.Entry(contact).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(contact)
        End Function

        ' GET: Contacts/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim contact As Contact = db.Contacts.Find(id)
            If IsNothing(contact) Then
                Return HttpNotFound()
            End If
            Return View(contact)
        End Function

        ' POST: Contacts/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim contact As Contact = db.Contacts.Find(id)
            db.Contacts.Remove(contact)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

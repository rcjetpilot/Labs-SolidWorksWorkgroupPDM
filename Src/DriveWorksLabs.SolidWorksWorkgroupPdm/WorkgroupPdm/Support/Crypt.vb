' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.Security.Cryptography

NotInheritable Class Crypt
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Encrypts a string by using the Windows Data Protection API scoped to the current user.
    ''' </summary>
    ''' <param name="uncryptedString">The string to encrypt.</param>
    ''' <returns>The encrypted string.</returns>
    ''' <remarks></remarks>
    Public Shared Function WeakEncrypt(ByVal uncryptedString As String) As String

        ' Nothing to do if there's nothing to work with
        If uncryptedString Is Nothing Then
            Return Nothing
        End If

        ' Encryption function works with bytes, so get a byte representation of the string
        Dim bytes = System.Text.Encoding.UTF8.GetBytes(uncryptedString)

        ' Use the data protection API built into Windows to perform the actual encryption
        ' such that only the current user can decrypt
        Dim encrypted = ProtectedData.Protect(bytes, Nothing, DataProtectionScope.CurrentUser)

        ' Convert to a string that we can turn back to bytes later on
        Dim encryptedString = Convert.ToBase64String(encrypted)

        ' All done
        Return encryptedString
    End Function

    ''' <summary>
    ''' Decrypts a string by using the Windows Data Protection API scoped to the current user.
    ''' </summary>
    ''' <param name="encryptedString">The string to decrypt.</param>
    ''' <returns>The decrypted string.</returns>
    ''' <remarks></remarks>
    Public Shared Function WeakDecrypt(ByVal encryptedString As String) As String

        ' Nothing to do if there's nothing to work with
        If encryptedString Is Nothing Then
            Return Nothing
        End If

        ' We store the encrypted data in the registry as a base-64 encoded string, so convert back to the bytes
        ' that the decryption API is expecting
        Dim encryptedBytes = Convert.FromBase64String(encryptedString)

        ' Use the data protection API built into Windows to perform the actual decryption
        ' - the parameters here must match the ones we used for encryption
        Dim decryptedBytes = ProtectedData.Unprotect(encryptedBytes, Nothing, DataProtectionScope.CurrentUser)

        ' The encrypted byes are the UTF8 bytes representing the string, so decode them now
        Dim decryptedString = System.Text.Encoding.UTF8.GetString(decryptedBytes)

        ' All done
        Return decryptedString
    End Function
End Class

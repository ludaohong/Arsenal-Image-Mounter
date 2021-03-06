﻿
''''' DeviceObject.vb
''''' Base class for Arsenal Image Mounter SCSI Miniport objects.
''''' 
''''' Copyright (c) 2012-2013, Arsenal Consulting, Inc. (d/b/a Arsenal Recon) <http://www.ArsenalRecon.com>
''''' This source code is available under the terms of the Affero General Public
''''' License v3.
'''''
''''' Please see LICENSE.txt for full license terms, including the availability of
''''' proprietary exceptions.
''''' Questions, comments, or requests for clarification: http://ArsenalRecon.com/contact/
'''''



''' <summary>
''' Base class that represents Arsenal Image Mounter SCSI miniport created device objects.
''' </summary>
Public MustInherit Class DeviceObject
    Implements IDisposable

    Public ReadOnly SafeFileHandle As SafeFileHandle
    Public ReadOnly AccessMode As FileAccess

    ''' <summary>
    ''' Opens specified Path with CreateFile Win32 API and encapsulates the returned handle
    ''' in a new DeviceObject.
    ''' </summary>
    ''' <param name="Path">Path to pass to CreateFile API</param>
    ''' <param name="AccessMode">Access mode for opening and for underlying FileStream</param>
    Protected Sub New(Path As String, AccessMode As FileAccess)
        Me.New(NativeFileIO.OpenFileHandle(Path, AccessMode, FileShare.ReadWrite, FileMode.Open, Overlapped:=False), AccessMode)
    End Sub

    ''' <summary>
    ''' Encapsulates a handle in a new DeviceObject.
    ''' </summary>
    ''' <param name="Handle">Existing handle to use</param>
    ''' <param name="Access">Access mode for underlying FileStream</param>
    Protected Sub New(Handle As SafeFileHandle, Access As FileAccess)
        SafeFileHandle = Handle
        AccessMode = Access
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            If SafeFileHandle IsNot Nothing Then
                SafeFileHandle.Dispose()
            End If

            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Close device object.
    ''' </summary>
    Public Sub Close() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


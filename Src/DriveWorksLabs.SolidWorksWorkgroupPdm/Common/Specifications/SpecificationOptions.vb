' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorks.Applications
Imports DriveWorks.Specification

Namespace CommonPdmFramework

    ''' <summary>
    ''' Provides options to customize the behavior of instances of the 
    ''' <see cref="SpecificationServiceHandler" /> class.
    ''' </summary>
    ''' <remarks></remarks>
    <Flags()>
    Public Enum SpecificationOptions

        ''' <summary>
        ''' The default, no specific additional behaviors are applied.
        ''' </summary>
        None = 0

        ''' <summary>
        ''' Specification files (e.g. the .drivespec, .drivemaster, .driveproj/.driveprojx files) shouldn't
        ''' be checked in.
        ''' </summary>
        ''' <remarks>
        ''' This is useful for PDM systems that only support checking in one file
        ''' with a given name even when the path is unique.
        ''' </remarks>
        DisableSpecificationFileCheckin = 1
    End Enum
End Namespace
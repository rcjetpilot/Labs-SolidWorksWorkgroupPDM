SolidWorks Workgroup PDM Plugin for DriveWorks
==============================================

Introduction
-----------------------------------------------
This is sample code which illustrates how 
to use the DriveWorks API to implement a simple
integration into SolidWorks Workgroup PDM.

This code is provided under the MIT license,
for more details see [LICENSE.md](https://github.com/DriveWorks/Labs-SolidWorks-WPDM/blob/master/LICENSE.md).

This sample code was built using:
- Microsoft Visual Studio 2010 SP1
- Windows Installer XML v3.5 (if you don't have this you can still use the source, but not build the installer)
- The DriveWorks 9 SDK (technically a standard DriveWorks 9 install will also work, but the SDK gives access to help)

This code was tested using:
- DriveWorks 9 SP0
- SolidWorks 2012 SP0 Workgroup PDM

Common PDM Framework
-----------------------------------------------
This plugin is built using an approach called the
common PDM framework - whose source files are in
the "Common" folder.

This source code framework is designed to make it
easier to build PDM plugins by focussing on implementing
a single interface (the IPdmInterface interface) by 
allowing the existing GenerationServiceHandler and 
SpecificationServiceHandler handler to do the work
of figuring out which files need interacting with PDM.

Because the framework is part of the source code it can
also be modified if necessary.
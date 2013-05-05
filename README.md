sdrsharp_hackrf
===============

SDR# HackRF plugin beta
In this version configuration of HackRF is really limited only change the sample rate and do not change other parameters.

SDR# is developped by Youssef Touil and available on 
http://sdrsharp.com

SDR# Source code:
http://sdrsharp.com/index.php/source-code
https://subversion.assembla.com/svn/sdrsharp

Required libs:
1) LibusbX 1.0.15 (or later but tested only with this one)
http://sourceforge.net/projects/libusbx/files/releases/1.0.15/binaries/libusbx-1.0.15-win.7z/download
Use the file libusbx-1.0.15-win.7z\libusbx-1.0.15\MinGW32\dll\libusb-1.0.dll
2) Install MinGW (especially to rebuild libhackrf and for pthreadGC2.dll)
3) libhackrf.dll (from https://github.com/mossmann/hackrf/tree/master/host/libhackrf)
To be built in directory hackrf/host/libhackrf/src
4) Install SDRSharp latest trunk from http://sdrsharp.com/downloads/sdr-install.zip
Install it in for example sdrsharp_hackrf\bin\sdrsharp directory

How to build and use SDR# + HackRF plugin:
1) Checkout sdrsharp from https://subversion.assembla.com/svn/sdrsharp
2) Copy sdrsharp_hackrf\src\HackRF directory and files to sdrsharp_hackrf\bin\sdrsharp\
3) Launch SDRSharp.sln and add project HackRF
4) Choose Release and x86 and Clean Solution then Rebuild Solution
 -> Now all is built in directory sdrsharp\trunk\Release
5) Copy sdrsharp\trunk\Release\* (except *.pdb/manifest) to sdrsharp_hackrf\bin\sdrsharp\* (from Install SDRSharp)
6) Edit file sdrsharp_hackrf\bin\sdrsharp\SDRSharp.exe.config and add the following line after line "<frontendPlugins>":
    <add key="HackRF / USB" value="SDRSharp.HackRF.HackRFIO,SDRSharp.HackRF" />
7) Extract libusbx-1.0.15-win.7z\libusbx-1.0.15\MinGW32\dll\libusb-1.0.dll to sdrsharp_hackrf\bin\sdrsharp\
8) Copy hackrf/host/libhackrf/src/libhackrf.dll to sdrsharp_hackrf\bin\sdrsharp\
9) Copy from mingw/bin/pthreadGC2.dll to sdrsharp_hackrf\bin\sdrsharp\





# Media Foundation .Net Extension Library v1.0

Media Foundation .Net Extention Library (mfext for short) is an optionnal library that enhance the Media Foundation .Net Library and make it a little more managed friendly.
mfext bring three kinds of enhancements:
  - Numerous extensions methods for 47 mflib's interfaces
  - A smarter MF static class for all Media Foundation functions
  - Some helpers classes to make interop easier

## Extensions methods
> Extension methods enable you to "add" methods to existing types without creating a new derived type, recompiling, or otherwise modifying the original type. MSDN Library <https://msdn.microsoft.com/en-us/library/bb383977%28v=vs.140%29.aspx>

For the purpose of mfext, the extension methods add methods to mflib's COM interfaces. These methods provide:
  - Better parameters passing, for example by removing parameters that only accept one (like null) or a limited number of values or by using managed types (like the TimeSpan structure to express duration).
  - Directly accept a managed Stream instead of a COM IStream.
  - Expose as methods features that are normaly only available with MF functions.

## The MF static class
This class is a wrapper around the mflib's MFExtern class but it add a lot of enhancements in the same vein as for the extensions methods.

For exemple instead of calling:
```c#
MFExtern.MFStartup(0x20070, MFStartup.Full);
```
You just have to call the following code to get the same result:
```c#
MF.Startup();
```
## The HResult extensions methods
mfext also come with enhancements to the HResult enumeration returned by most mflib's methods.
You can now write code like:
```c#
HResult hr = MF.GetSupportedMimeTypes(supportedMimeTypes);
if (hr.Succeeded()) 
{
    // Do something with supportedMimeTypes
}
```
Or if you prefer working with exceptions
```c#
HResult hr = MF.GetSupportedMimeTypes(supportedMimeTypes);
hr.ThrowExceptionOnError(); 
```
## Helpers classes from the Interop namespace
  - ComCallableStream: A class that expose the COM interface IStream around a wrapped managed Stream.
  - RuntimeCallableStream: A class that expose a managed Stream around a wrapped IStream COM interface
  - GCPin: A wrapper around GCHandle that implement the Disposable pattern.
  - MemoryBuffer:  A class based on SafeBuffer that allow to easily read or write into unmanaged memory.
  

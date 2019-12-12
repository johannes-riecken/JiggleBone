import core.sys.linux.dlfcn;
import core.stdc.stdio;
import core.stdc.string;
import core.vararg;
import std.string;
import std.path;
import std.file;
import core.stdc.stdlib;

extern (C) int coreclr_initialize(const (char)* exePath, const (char)* appDomainFriendlyName, int propertyCount, const (char)** propertyKeys, const (char)** propertyValues, void** hostHandle, uint* domainId);
alias coreclr_initialize_ptr = extern (C) int function(const (char)* exePath, const (char)* appDomainFriendlyName, int propertyCount, const (char)** propertyKeys, const (char)** propertyValues, void** hostHandle, uint* domainId);

extern (C) int coreclr_shutdown(void* hostHandle, uint domainId);
alias coreclr_shutdown_ptr = extern (C) int function(void* hostHandle, uint domainId);

extern (C) int coreclr_create_delegate(void* hostHandle, uint domainId, const (char)* entryPointAssemblyName, const (char)* entryPointTypeName, const (char)* entryPointMethodName, void** deleg);
alias coreclr_create_delegate_ptr = extern (C) int function(void* hostHandle, uint domainId, const (char)* entryPointAssemblyName, const (char)* entryPointTypeName, const (char)* entryPointMethodName, void** deleg);

extern (C) int coreclr_execute_assembly(void* hostHandle, uint domainId, int argc, const (char)** argv, const (char)* managedAssemblyPath, uint* exitCode);
alias coreclr_execute_assembly_ptr = extern (C) int function(void* hostHandle, uint domainId, int argc, const (char)** argv, const (char)* managedAssemblyPath, uint* exitCode);

int main() {
    void *hostHandle;
    uint domainId;
    uint exitcode = 0;

    const (char) **propertyKeys = cast(const(char)**)malloc((char*).sizeof * 2);
    propertyKeys[0] = "APP_PATHS";
    propertyKeys[1] = "TRUSTED_PLATFORM_ASSEMBLIES";
    const (char) **propertyValues = cast(const(char)**)malloc((char*).sizeof * 2);
    propertyValues[0] = "/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1";
    // TODO: char* dest = cast(char*)calloc(50, char.sizeof);
    // TODO: strcat(dest, cast(const(char)*)"Hello, ");
    // TODO: strcat(dest, cast(const(char)*)"World!");

    propertyValues[1] = cast(const(char)*)(getcwd() ~ "/bin/Debug/netcoreapp2.1/" ~ baseName(getcwd()) ~ ".dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Runtime.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/mscorlib.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Drawing.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Console.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.IO.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Threading.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.IO.FileSystem.Primitives.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Runtime.Handles.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Text.Encoding.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Runtime.Extensions.dll:/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/System.Threading.Tasks.dll");
    void *pLib = dlopen("/home/jo/csharp/coreclr/Tools/dotnetcli/shared/Microsoft.NETCore.App/2.1.1/libcoreclr.so",RTLD_NOW|RTLD_LOCAL);
    coreclr_initialize_ptr initPtr = cast(coreclr_initialize_ptr)dlsym(pLib,"coreclr_initialize");
    const(char)[] error;
    assert((error = fromStringz(dlerror())) == null, error);
    coreclr_execute_assembly_ptr execPtr = cast(coreclr_execute_assembly_ptr)dlsym(pLib,"coreclr_execute_assembly");
    coreclr_shutdown_ptr shutdownPtr = cast(coreclr_shutdown_ptr)dlsym(pLib,"coreclr_shutdown");
    coreclr_create_delegate_ptr delegatePtr = cast(coreclr_create_delegate_ptr)dlsym(pLib, "coreclr_create_delegate");
    assert (pLib, fromStringz(dlerror()));
    assert(delegatePtr != null, "Creating delegatePtr failed");
    int st = initPtr("/home/jo/csharp/coreclr/Tools/dotnetcli/host","host",2,propertyKeys,propertyValues,&hostHandle,&domainId);
    printf("state: %x\n",st);
    assert(st >= 0, "initPtr returned invalid state");
    assert(hostHandle != null, "assertion failed: hostHandle != null");
    const (char) *entryPointAssemblyName = "JiggleBone";
    const (char) *entryPointTypeName = "Quaternion";

    const (char) *entryPointMethodName = "IsEqualUsingDot";
    extern (C) bool function(float) pfnIsEqualUsingDot;
    st = delegatePtr(hostHandle, domainId, entryPointAssemblyName, entryPointTypeName, entryPointMethodName, cast(void**)(&pfnIsEqualUsingDot));
    assert (st >= 0, "delegatePtr pfnIsEqualUsingDot returned invalid state: " ~ format("%8x",st));
    // TODO: var res = pfnIsEqualUsingDot(...);
    printf("res1: %d\nres2: %d\n", pfnIsEqualUsingDot(1.0f), pfnIsEqualUsingDot(0.5f));

    shutdownPtr(hostHandle,domainId);
    dlclose(pLib);
    return 0;
}

// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <IPlatform.h>
#include <ICPUEx.h>
#include <IDeviceManager.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" __declspec(dllexport) IPlatform * GetPlatformWrapper() {
    return &GetPlatform();
}

extern "C" __declspec(dllexport) bool Platform_Init(IPlatform * platform) {
    return platform->Init();
}

extern "C" __declspec(dllexport) bool Platform_UnInit(IPlatform * platform) {
    return platform->UnInit();
}

extern "C" __declspec(dllexport) IDeviceManager * Platform_GetDeviceManager(IPlatform * platform) {
    return &platform->GetIDeviceManager();
}

extern "C" __declspec(dllexport) ICPUEx * DeviceManager_GetCPU(IDeviceManager * deviceManager) {
    return (ICPUEx*)deviceManager->GetDevice(dtCPU, 0);
}

extern "C" __declspec(dllexport) unsigned int CPU_GetCoreCount(ICPUEx * cpuDevice) {
    unsigned int uCoreCount = 0;
    cpuDevice->GetCoreCount(uCoreCount);
    return uCoreCount;
}

extern "C" __declspec(dllexport) float CPU_fPPTValue(ICPUEx * cpuDevice) {
    CPUParameters parameters;
    cpuDevice->GetCPUParameters(parameters);
    return parameters.fPPTValue;
}

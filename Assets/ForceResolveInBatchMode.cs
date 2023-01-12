// ForceResolveInBatchMode: The utility class to force resolve vpm dependencies in batch mode.
// Originally (c) anatawa12 2023 but no copyright notice is required.
//
// Upstream VPM doesn't resolve dependencies by default because it may adds external/unsafe code
// However, In batch mode, asking to user does not work and resolve is not proceed.
// So, I made this class. When you add this class to your codebase, if unity is in batch mode,
// this class will call vpm-resolver to resolve packages without asking users.
//
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <https://unlicense.org>

#if UNITY_EDITOR && false // temporary disable

using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Anatawa12.Utils
{
    [InitializeOnLoad]
    public static class ForceResolveInBatchMode {
        static ForceResolveInBatchMode()
        {
            if (Application.isBatchMode) {
                CallResolver();
            }
        }

        public static void CallResolver()
        {
            Debug.Log("Batch mode detected. Invoking VPMProjectManifest.Resolve...");
            try
            {
                // first, call VPMProjectManifest.Resolve
                {
                    var asm = Assembly.Load("vpm-core-lib");
                    var type = asm.GetType("VRC.PackageManagement.Core.Types.Packages.VPMProjectManifest");
                    var method = type.GetMethod("Resolve", BindingFlags.Public | BindingFlags.Static, null,
                        new[] { typeof(string) }, null);
                    method.Invoke(null, new object[] { Directory.GetCurrentDirectory() });
                }
                // first, call UnityEditor.PackageManager.Client.Resolve
                {
                    var method = typeof(UnityEditor.PackageManager.Client)
                        .GetMethod("Resolve", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    if (method != null)
                        method.Invoke(null, null);
                }
                return;
            }
            catch (Exception e)
            {
                Debug.LogError("Invoking VPMProjectManifest.Resolve failed. ");
                Debug.LogError(e);
            }
        }
    }
}

#endif

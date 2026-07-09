using Elements.Core;
using FrooxEngine;
using FrooxEngine.FrooxEngine.ProtoFlux.CoreNodes;
using FrooxEngine.ProtoFlux;
using FrooxEngine.UIX;
using HarmonyLib;
using System.Reflection;

namespace BepisDynamicVariablePowerTools.Patches;

[HarmonyPatch]
public static class RenameDynvarSources
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ProtoFluxNodeVisual), "BuildUI")]
    public static void RenameSource(ProtoFluxNodeVisual __instance)
    {
        if (!BepisDynamicVariablePowerTools.RenameDynvarSources.Value) return;

        var node = __instance.Node.Target;

        Type nodeType = node.GetType();
        Type baseType = nodeType.IsGenericType ? nodeType.GetGenericTypeDefinition() : nodeType;
        Type innerType = nodeType.IsGenericType ? nodeType.GenericTypeArguments[0] : nodeType;

        if (baseType == typeof(ValueSource<>))
        {
            typeof(RenameDynvarSources).GetGenericMethod("HandleValueSource", BindingFlags.Static | BindingFlags.Public, innerType).Invoke(null, [__instance, node]);
        }
        if (baseType == typeof(ReferenceSource<>))
        {
            typeof(RenameDynvarSources).GetGenericMethod("HandleReferenceSource", BindingFlags.Static | BindingFlags.Public, innerType).Invoke(null, [__instance, node]);
        }
    }

    public static void HandleValueSource<T>(ProtoFluxNodeVisual visual, ValueSource<T> source) where T : unmanaged
    {
        var refValue = (IValue<T>)source.RootSourceReference.Target;
        var parent = refValue.Parent;
        if (parent is Component c)
        {
            var compType = c.GetType();
            if (compType.IsGenericType)
            {
                if (compType == typeof(DynamicValueVariable<T>))
                {
                    string variableName = Traverse.Create(parent).Field<Sync<string>>("VariableName").Value;
                    ReplaceNodeText(visual, c, variableName);
                }
            }
        }
    }

    public static void HandleReferenceSource<T>(ProtoFluxNodeVisual visual, ReferenceSource<T> source) where T : class, IWorldElement
    {
        var refValue = (SyncRef<T>)source.RootSourceReference.Target;
        var parent = refValue.Parent;
        if (parent is Component c)
        {
            var compType = c.GetType();
            if (compType.IsGenericType)
            {
                if (compType == typeof(DynamicReferenceVariable<T>))
                {
                    string variableName = Traverse.Create(parent).Field<Sync<string>>("VariableName").Value;
                    ReplaceNodeText(visual, c, variableName);
                }
            }
        }
    }

    public static void ReplaceNodeText(ProtoFluxNodeVisual visual, Component component, string VariableName)
    {
        Slot holder = visual.Slot.FindChildInHierarchy("Overlapping Layout").FindChild("Horizontal Layout").Children.First();
        holder.GetComponent<Text>().Content.Value = $"<size=60%>Dynamic Variable:</size>\n<i>{VariableName}</i><size=75%>\n(On: {component.Slot.Name})</size>";
    }
}

using FrooxEngine;
using FrooxEngine.FrooxEngine.ProtoFlux.CoreNodes;
using FrooxEngine.ProtoFlux;
using FrooxEngine.UIX;
using HarmonyLib;
using System.Reflection;

namespace BepisDynamicVariablePowerTools.Patches;

[HarmonyPatch]
public static class RenameDynVarNodes
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ProtoFluxNodeVisual), "BuildUI")]
    public static void RenameNode(ProtoFluxNodeVisual __instance)
    {
        if (!BepisDynamicVariablePowerTools.RenameDynvarNodes.Value) return;

        var node = __instance.Node.Target;

        Type nodeType = node.GetType();
        Type baseType = nodeType.IsGenericType ? nodeType.GetGenericTypeDefinition() : nodeType;
        if (baseType == typeof(ValueSource<>) || baseType == typeof(ObjectValueSource<>) || baseType == typeof(ReferenceSource<>))
        {
            IValue sourceTarget = (IValue)Traverse.Create(node).Property<ISyncRef>("RootSourceReference").Value.Target;
            ReplaceNodeText(__instance, sourceTarget);
        }
        if (baseType == typeof(ValueFieldDrive<>) || baseType == typeof(ObjectFieldDrive<>))
        {
            MethodInfo getRootProxy = nodeType.GetMethod("GetRootProxy");
            dynamic proxy = getRootProxy.Invoke(node, new object[] { false });
            IValue driveTarget = proxy?.Drive.Target;
            ReplaceNodeText(__instance, driveTarget);
        }
        if (baseType == typeof(ReferenceDrive<>))
        {
            MethodInfo _getRefProxy = typeof(RenameDynVarNodes).GetMethod(nameof(RenameDynVarNodes.GetRefProxy));
            Type nodeInnerType = nodeType.GetGenericArguments()[0];
            MethodInfo getRefProxy = _getRefProxy?.MakeGenericMethod(nodeInnerType);
            dynamic proxy = getRefProxy.Invoke(null, new object[] { node });
            IValue driveTarget = proxy?.Drive.Target;
            ReplaceNodeText(__instance, driveTarget);
        }
    }

    public static FrooxEngine.ProtoFlux.CoreNodes.ReferenceDrive<T>.Proxy GetRefProxy<T>(ProtoFluxNode node) where T : class, IWorldElement
    {
        return node.Slot.GetComponent((global::FrooxEngine.ProtoFlux.CoreNodes.ReferenceDrive<T>.Proxy p) => p.Node.Target == node);
    }

    public static void ReplaceNodeText(ProtoFluxNodeVisual visual, IValue Target)
    {
        string fieldName = Target?.Name;
        var parent = Target?.Parent;
        Traverse field = Traverse.Create(parent).Field("VariableName");
        if (field.FieldExists())
        {
            Component component = (Component)parent;
            string variableName = field.GetValue<Sync<string>>().Value;
            Slot holder = visual.Slot.FindChildInHierarchy("Overlapping Layout").FindChild("Horizontal Layout").Children.First();
            holder.GetComponent<Text>().Content.Value = $"<size=60%>Dynamic Variable: ({fieldName})</size>\n<i>{variableName}</i><size=75%>\n(On: {component.Slot.Name})</size>";
        }
    }
}

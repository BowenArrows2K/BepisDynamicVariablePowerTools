using Elements.Core;
using FrooxEngine;
using FrooxEngine.FrooxEngine.ProtoFlux.CoreNodes;
using FrooxEngine.ProtoFlux;
using FrooxEngine.UIX;
using HarmonyLib;

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

        if (baseType == typeof(ValueSource<>) || baseType == typeof(ObjectValueSource<>) || baseType == typeof(ReferenceSource<>))
        {
            IValue sourceTarget = (IValue)Traverse.Create(node).Property<ISyncRef>("RootSourceReference").Value.Target;

            var fieldName = sourceTarget.Name;
            var parent = sourceTarget.Parent;
            var field = Traverse.Create(parent).Field("VariableName");
            if (field.FieldExists())
            {
                string variableName = field.GetValue<Sync<string>>().Value;
                ReplaceNodeText(__instance, (Component)parent, variableName, fieldName);
            }
        }
    }

    public static void ReplaceNodeText(ProtoFluxNodeVisual visual, Component component, string VariableName, string FieldName)
    {
        Slot holder = visual.Slot.FindChildInHierarchy("Overlapping Layout").FindChild("Horizontal Layout").Children.First();
        holder.GetComponent<Text>().Content.Value = $"<size=60%>Dynamic Variable: ({FieldName})</size>\n<i>{VariableName}</i><size=75%>\n(On: {component.Slot.Name})</size>";
    }
}

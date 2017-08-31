using System;
using System.Collections.Generic;
using ApiChange.Api.Introspection;
using Mono.Cecil;

namespace ApiChange.Api.Extensions
{
  class DefaultApiBreakRuleStrategy : IApiBreakRuleStrategy
  {
    public ApiBreakLevel GetBreakLevel(AssemblyDiffCollection diffResult)
    {
      ApiBreakLevel wDefaultBreakLevel = ApiBreakLevel.None;

      // MAJOR: check for removed types...
      if (diffResult.AddedRemovedTypes.RemovedCount > 0)
        wDefaultBreakLevel = ApiBreakLevel.Major;

      foreach (TypeDiff changedType in diffResult.ChangedTypes)
      {
        // MAJOR: interface change...
        if (changedType.TypeV1.IsInterface)
          wDefaultBreakLevel = ApiBreakLevel.Major;
        else if (changedType.TypeV1.IsEnum)
        {
          // MAJOR: Removed enum constant...
          List<FieldDefinition> removedEnumConstants = changedType.Fields.RemovedList;
          if (removedEnumConstants.Count > 0)
            wDefaultBreakLevel = ApiBreakLevel.Major;
        }
        else
        {
          // MAJOR: Removed event...
          List<EventDefinition> removedEvents = changedType.Events.RemovedList;
          if (removedEvents.Count > 0)
            wDefaultBreakLevel = ApiBreakLevel.Major;

          // MAJOR: Removed method...
          List<MethodDefinition> removedMethods = changedType.Methods.RemovedList;
          if (removedMethods.Count > 0)
            wDefaultBreakLevel = ApiBreakLevel.Major;

          // MAJOR: Public fields removed...
          if (changedType.Fields.RemovedList.Count > 0)
            wDefaultBreakLevel = ApiBreakLevel.Major;

          // MAJOR: Class does not implement interface anymore...
          if (changedType.Interfaces.RemovedCount > 0)
            wDefaultBreakLevel = ApiBreakLevel.Major;

          // MAJOR: Class does not inherit the same class as before...
          if (changedType.HasChangedBaseType)
            wDefaultBreakLevel = ApiBreakLevel.Major;
        }
      }

      // check for minor changes...
      if (wDefaultBreakLevel == ApiBreakLevel.None)
      {
        if (diffResult.AddedRemovedTypes.AddedCount > 0 ||
            diffResult.ChangedTypes.Count > 0)
        {
          wDefaultBreakLevel = ApiBreakLevel.Minor;
        }
      }

      return wDefaultBreakLevel;
    }
  }
}
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Driving.Models;

namespace Shared.Core;

// TODO create code snippet for unittest
// TODO create code snippet for EntityConfig
// TODO create code snippet for Entity
// TODO research Character.CSharp.Shared.Core.sln.DotSettings.user and Character.CSharp.Shared.Core.sln.DotSettings

// TODO develop code review check list
/*
 * Rule validators should be in the appropriate order
 * Rule validators should have Message, PropertyName, ErrorCode
 * Rule message should include property name inside a '', and it should end with a dot (.)
 * Multiple rules or validators those indicate one singe ErrorCode and Message should be merge into a single one
 * TODO the first character of PropertyName should be lowercase
 */
public static class ServiceCollectionExt
{
    public static void AddSharedCoreService(this IServiceCollection services, Assembly currentAssembly)
    {
        #region FluentValidation

        // TODO need to explain this FluentValidationMessagePartToRemove
        ValidatorOptions.Global.DisplayNameResolver = (_, _, _) => ErrorResponse.FluentValidationMessagePartToRemove;
        services.AddValidatorsFromAssembly(currentAssembly);

        #endregion
    }
}
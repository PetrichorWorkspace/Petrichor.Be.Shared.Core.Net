using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.Driven.Validators;

public static class ServiceCollectionExt
{
    public static void AddFluentValidationServices(this IServiceCollection services, Assembly currentAssembly)
    {
        ConfigFluent();
        services.AddValidatorsFromAssembly(currentAssembly);
    }

    // TODO we have this for unittest
    public static void ConfigFluent()
    {
        ValidatorOptions.Global.DisplayNameResolver = SetPropertyResolvers;
        ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
    }
    
    private static string? SetPropertyResolvers(Type type, MemberInfo memberInfo, LambdaExpression lambdaExpression)
    {
        var jsonPropertyNameAttribute = memberInfo?
            .GetCustomAttributes<JsonPropertyNameAttribute>()
            .FirstOrDefault();

        if (jsonPropertyNameAttribute != null)
            return jsonPropertyNameAttribute.Name; // Render custom JSON property name

        return memberInfo != null
            ? memberInfo.Name
            : null; // Render default display name
    }
    
    private class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager() 
        {
            AddTranslation("en", "NotNullValidator", "Must not be null.");
            AddTranslation("en", "NotEmptyValidator", "Must not be empty.");
            AddTranslation("en", "MinimumLengthValidator", "The length must be longer."); // TODO the message is not good
            AddTranslation("en", "MaximumLengthValidator", "The length must be shorter."); // TODO the message is not good
            AddTranslation("en", "LessThanOrEqualToValidator", "Must be less than or equal to xxx."); // TODO the message is not good
        }
    }
}
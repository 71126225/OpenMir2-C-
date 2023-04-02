using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SystemModule.CoreSocket;

/// <summary>
/// 表示属性的Getter
/// </summary>
public class MemberGetter
{
    /// <summary>
    /// get方法委托
    /// </summary>
    private readonly Func<object, object> m_getFunc;

    /// <summary>
    /// 表示属性的Getter
    /// </summary>
    /// <param name="property">属性</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MemberGetter(PropertyInfo property)
    {
        m_getFunc = CreateGetterDelegate(property);
    }

    /// <summary>
    /// 表示类型字段或属性的Getter
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public MemberGetter(FieldInfo fieldInfo)
    {
        m_getFunc = CreateGetterDelegate(fieldInfo);
    }

    /// <summary>
    /// 获取属性的值
    /// </summary>
    /// <param name="instance">实例</param>
    /// <returns></returns>
    public object Invoke(object instance)
    {
        return m_getFunc.Invoke(instance);
    }

    private static Func<object, object> CreateGetterDelegate(PropertyInfo property)
    {
        ParameterExpression param_instance = Expression.Parameter(typeof(object));
        UnaryExpression body_instance = Expression.Convert(param_instance, property.DeclaringType);
        MemberExpression body_property = Expression.Property(body_instance, property);
        UnaryExpression body_return = Expression.Convert(body_property, typeof(object));

        return Expression.Lambda<Func<object, object>>(body_return, param_instance).Compile();
    }

    private static Func<object, object> CreateGetterDelegate(FieldInfo fieldInfo)
    {
        ParameterExpression param_instance = Expression.Parameter(typeof(object));
        UnaryExpression body_instance = Expression.Convert(param_instance, fieldInfo.DeclaringType);
        MemberExpression body_field = Expression.Field(body_instance, fieldInfo);
        UnaryExpression body_return = Expression.Convert(body_field, typeof(object));

        return Expression.Lambda<Func<object, object>>(body_return, param_instance).Compile();
    }
}
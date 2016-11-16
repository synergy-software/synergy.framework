//// Decompiled with JetBrains decompiler
//// Type: System.Web.Mvc.ExpressionUtil.CachedExpressionCompiler
//// Assembly: System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
//// MVID: CC73190B-AB9D-435C-8315-10FF295C572A

//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Reflection;

//namespace System.Web.Mvc.ExpressionUtil
//{
//    internal static class CachedExpressionCompiler
//    {
//        public static Func<TModel, TValue> Process<TModel, TValue>(Expression<Func<TModel, TValue>> lambdaExpression)
//        {
//            return CachedExpressionCompiler.Compiler<TModel, TValue>.Compile(lambdaExpression);
//        }

//        private static class Compiler<TIn, TOut>
//        {
//            private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> _simpleMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();
//            private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> _constMemberAccessDict = new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();
//            private static readonly ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>> _fingerprintedCache = new ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>();
//            private static Func<TIn, TOut> _identityFunc;

//            public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
//            {
//                return CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromIdentityFunc(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromConstLookup(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromMemberAccess(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileFromFingerprint(expr) ?? CachedExpressionCompiler.Compiler<TIn, TOut>.CompileSlow(expr);
//            }

//            private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
//            {
//                ConstantExpression body = expr.Body as ConstantExpression;
//                if (body == null)
//                    return (Func<TIn, TOut>)null;
//                TOut constantValue = (TOut)body.Value;
//                return (Func<TIn, TOut>)(_ => constantValue);
//            }

//            private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
//            {
//                if (expr.Body != expr.Parameters[0])
//                    return (Func<TIn, TOut>)null;
//                if (CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc == null)
//                    CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc = expr.Compile();
//                return CachedExpressionCompiler.Compiler<TIn, TOut>._identityFunc;
//            }

//            private static Func<TIn, TOut> CompileFromFingerprint(Expression<Func<TIn, TOut>> expr)
//            {
//                List<object> capturedConstants;
//                ExpressionFingerprintChain fingerprintChain = FingerprintingExpressionVisitor.GetFingerprintChain((Expression)expr, out capturedConstants);
//                if (fingerprintChain == null)
//                    return (Func<TIn, TOut>)null;
//                Hoisted<TIn, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._fingerprintedCache.GetOrAdd(fingerprintChain, (Func<ExpressionFingerprintChain, Hoisted<TIn, TOut>>)(_ => HoistingExpressionVisitor<TIn, TOut>.Hoist(expr).Compile()));
//                return (Func<TIn, TOut>)(model => del(model, capturedConstants));
//            }

//            private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
//            {
//                MemberExpression memberExpr = expr.Body as MemberExpression;
//                if (memberExpr != null)
//                {
//                    if (memberExpr.Expression == expr.Parameters[0] || memberExpr.Expression == null)
//                        return CachedExpressionCompiler.Compiler<TIn, TOut>._simpleMemberAccessDict.GetOrAdd(memberExpr.Member, (Func<MemberInfo, Func<TIn, TOut>>)(_ => expr.Compile()));
//                    ConstantExpression expression = memberExpr.Expression as ConstantExpression;
//                    if (expression != null)
//                    {
//                        ParameterExpression parameterExpression;
//                        Func<object, TOut> del = CachedExpressionCompiler.Compiler<TIn, TOut>._constMemberAccessDict.GetOrAdd(memberExpr.Member, (Func<MemberInfo, Func<object, TOut>>)(_ => Expression.Lambda<Func<object, TOut>>((Expression)memberExpr.Update((Expression)Expression.Convert((Expression)parameterExpression, memberExpr.Member.DeclaringType)), new ParameterExpression[1]
//                       {
//              parameterExpression
//                       }).Compile()));
//                        object capturedLocal = expression.Value;
//                        return (Func<TIn, TOut>)(_ => del(capturedLocal));
//                    }
//                }
//                return (Func<TIn, TOut>)null;
//            }

//            private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
//            {
//                return expr.Compile();
//            }
//        }
//    }
//}

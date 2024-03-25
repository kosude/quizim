/*
 *   Copyright (c) 2024 Jack Bennett.
 *   All Rights Reserved.
 *
 *   See the LICENCE file for more information.
 */

using System.Text.RegularExpressions;

namespace Quizim.Api.Conventions
{
    /// <summary>
    /// Converts e.g. TestControllerName to test-controller-name
    /// </summary>
    public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return value == null ? null : NextWordRegex().Replace(value.ToString()!, "$1-$2").ToLower();
        }

        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex NextWordRegex();
    }
}

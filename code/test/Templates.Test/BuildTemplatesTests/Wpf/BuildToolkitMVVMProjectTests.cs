﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;

using Microsoft.TemplateEngine.Abstractions;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Extensions;
using Xunit;

namespace Microsoft.Templates.Test.Build.Wpf
{
    [Collection("BuildTemplateTestCollection")]
    public class BuildMVVMToolkitProjectTests : BaseGenAndBuildTests
    {
        public BuildMVVMToolkitProjectTests(BuildTemplatesTestFixture fixture)
            : base(fixture, null, Frameworks.MVVMToolkit)
        {
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildProjects")]
        public async Task Build_EmptyProject_WpfAsync(string projectType, string framework, string platform, string language)
        {
            var (projectName, projectPath) = await GenerateEmptyProjectAsync(projectType, framework, platform, language);

            AssertBuildProject(projectPath, projectName, platform, true);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeaturesWpf")]
        [Trait("Type", "BuildRandomNamesWpf")]
        public async Task Build_All_ProjectNameValidation_G1_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, "", Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeaturesWpf")]
        [Trait("Type", "BuildRandomNamesWpf")]
        public async Task Build_All_ProjectNameValidation_G2_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                 && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "MinimumWpf")]
        [Trait("ExecutionSet", "MinimumMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyleWpf")]
        public async Task Build_All_CheckWithStyleCop_G1_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllG1";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName, false);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "MinimumWpf")]
        [Trait("ExecutionSet", "MinimumMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_CIBuild")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyleWpf")]
        public async Task Build_All_CheckWithStyleCop_G2_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                && t.Identity != "wts.Wpf.Feat.MSIXPackaging"
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllG2";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName, true);

            AssertBuildProject(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        public async Task Build_AllWithMsix_G1_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group2.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllMsix";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectWpfWithMsix(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), Frameworks.MVVMToolkit, ProgrammingLanguages.CSharp, Platforms.Wpf)]
        [Trait("ExecutionSet", "BuildMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_Full")]
        public async Task Build_AllWithMsix_G2_WpfAsync(string projectType, string framework, string platform, string language)
        {
            bool templateSelector(ITemplateInfo t) => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplates_Wpf_Group1.Contains(t.GroupIdentity)
                && !t.GetIsHidden()
                || t.Identity == "wts.Wpf.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllMsix";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectWpfWithMsix(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetPageAndFeatureTemplatesForBuild), Frameworks.MVVMToolkit, ProgrammingLanguages.CSharp, Platforms.Wpf, "wts.Wpf.Feat.MSIXPackaging")]
        [Trait("ExecutionSet", "BuildOneByOneMVVMToolkitWpf")]
        [Trait("ExecutionSet", "_OneByOne")]
        [Trait("Type", "BuildOneByOneMVVMToolkitWpf")]
        public async Task Build_MVVMBasic_OneByOneItems_WpfAsync(string itemName, string projectType, string framework, string platform, string itemId, string language)
        {
            var (ProjectPath, ProjecName) = await AssertGenerationOneByOneAsync(itemName, projectType, framework, platform, itemId, language, false);

            AssertBuildProject(ProjectPath, ProjecName, platform);
        }
    }
}

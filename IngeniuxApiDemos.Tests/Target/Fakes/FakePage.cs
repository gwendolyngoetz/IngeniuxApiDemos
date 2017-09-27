using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ingeniux.CMS;
using Ingeniux.CMS.Enums;
using Ingeniux.CMS.Event;
using Ingeniux.CMS.Presentation;

namespace SlickplanToIngeniux.Tests.Target.Fakes
{
    public class FakePage : IPage
    {
        public FakePage(string id)
        {
            Id = id;
        }

        public IAttribute Attribute(string name)
        {
            throw new NotImplementedException();
        }

        public string AttributeValue(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAttribute> Attributes()
        {
            throw new NotImplementedException();
        }

        public int AttributesCount()
        {
            throw new NotImplementedException();
        }

        public void SetAttributeValue(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void AddAttribute(params IAttribute[] attribute)
        {
            throw new NotImplementedException();
        }

        public void AddAttributes(IEnumerable<IAttribute> attributes)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAttribute(IAttribute attribute)
        {
            throw new NotImplementedException();
        }

        public void ClearAttributes()
        {
            throw new NotImplementedException();
        }

        public string ContentUnitTemplateName { get; }
        public string Id { get; }
        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public void ValidateUserPermission(IUser user)
        {
            throw new NotImplementedException();
        }

        public void ValidateData()
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
        public string CreationUser { get; }
        public DateTime Created { get; }
        public string LastModifiedUser { get; }
        public DateTime LastModified { get; }
        public IUserSession Session { get; }
        public Guid SessionId { get; }
        public ISite Manager { get; }
        public XDocument DOM()
        {
            throw new NotImplementedException();
        }

        public IElement Element(string name)
        {
            return new Element();
        }

        public IElement ElementById(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public IElement PresentationElement(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElement> Elements()
        {
            return new List<IElement>();
        }

        public IEnumerable<IElement> PresentationElements()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElement> Elements(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElement> AllElements(Func<IElement, bool> filterCallback = null)
        {
            throw new NotImplementedException();
        }

        public int ElementsCount()
        {
            throw new NotImplementedException();
        }

        public void MarkForPublish(IPublishingTarget pubTarget, bool recursive, bool mark)
        {
            throw new NotImplementedException();
        }

        public void AddElement(params IElement[] element)
        {
            throw new NotImplementedException();
        }

        public void AddElements(IEnumerable<IElement> elements)
        {
            throw new NotImplementedException();
        }

        public void AddElementFirst(params IElement[] element)
        {
            throw new NotImplementedException();
        }

        public void AddElementsFirst(IEnumerable<IElement> elements)
        {
            throw new NotImplementedException();
        }

        public void AddElementBefore(IElement anchor, params IElement[] element)
        {
            throw new NotImplementedException();
        }

        public void AddElementsBefore(IElement anchor, IEnumerable<IElement> elements)
        {
            throw new NotImplementedException();
        }

        public void AddElementAfter(IElement anchor, params IElement[] element)
        {
            throw new NotImplementedException();
        }

        public void AddElementsAfter(IElement anchor, IEnumerable<IElement> elements)
        {
            throw new NotImplementedException();
        }

        public bool RemoveElement(IElement element)
        {
            throw new NotImplementedException();
        }

        public void ClearElements()
        {
            throw new NotImplementedException();
        }

        public string XID { get; }
        public int VersionNumber { get; }
        public DateTime Date { get; }
        public IPage Page { get; }

        ISchemaVersion IPage.SchemaVersion
        {
            get { throw new NotImplementedException(); }
        }

        public IUser AssignedUser { get; }
        public IUserGroup AssignedGroup { get; }
        public ILocale Locale { get; set; }
        public bool LocaleInherited { get; }
        public ILingualPageMap MasterLingualMap { get; }
        public bool SerializeOnCheckIn { get; set; }
        public ISite Site { get; }
        public bool SecurityInherited { get; set; }
        public string AssignmentComment { get; }
        public IWorkflow CurrentWorkflow { get; }
        public IPageVersion LastVersion { get; }
        public bool RulesInherited { get; set; }
        public string PCRDescriptorID { get; }
        public IList<string> ReferenceMapping { get; }
        public bool CanParentRegionRoots { get; set; }

        public string Path()
        {
            throw new NotImplementedException();
        }

        public bool MarkedForPublish()
        {
            throw new NotImplementedException();
        }

        public bool MarkedForPublishOnTarget(IPublishingTarget pugTarget)
        {
            throw new NotImplementedException();
        }

        public bool MarkedForPublishOnTarget(string pugTargetId)
        {
            throw new NotImplementedException();
        }

        public bool MarkedForPublishPropagated()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPage> Children(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public int ChildrenCount()
        {
            throw new NotImplementedException();
        }

        public IPage PreviousSibling()
        {
            throw new NotImplementedException();
        }

        public IPage NextSibling()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageHierarchyAndID> SiblingIDs(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPage> Siblings(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageHierarchyAndID> AncestorsIDs(IHierarchyId rootHierarchy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPage> Ancestors(IHierarchyId rootHierarchy = null)
        {
            throw new NotImplementedException();
        }

        public int AncestorsCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPage> Descendants(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPage> DescendantsWithMaxDepth(out int count, int maxDepth, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageHierarchyAndID> DescendantsIDs(out int count, int maxCount = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageHierarchyAndID> DescendantsIDsWithMaxDepth(out int count, int maxDepth, int maxCount = -1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageSimpleHierarchy> DescendantsIDsWithMaxDepthForPreview(int maxDepth, int maxCount = -1)
        {
            throw new NotImplementedException();
        }

        public int DescendantsCount()
        {
            throw new NotImplementedException();
        }

        public bool IsDescendantOf(IPage ancestorPage)
        {
            throw new NotImplementedException();
        }

        public IPage Parent()
        {
            throw new NotImplementedException();
        }

        public IPage RegionRoot()
        {
            throw new NotImplementedException();
        }

        public IPageVersion CheckedInPage()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ILingualPageMap> CloneLingualMaps(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public int CloneLingualMapsCount()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void CheckInSingleWithNoValidate(IEnumerable<IPublishingTarget> pubTargets = null)
        {
            throw new NotImplementedException();
        }

        public void CheckInSingleWithNoValidateOnProfile(IPublishingProfile profile)
        {
            throw new NotImplementedException();
        }

        public int CheckIn(IEnumerable<IPublishingTarget> pubTargets, bool recursive)
        {
            throw new NotImplementedException();
        }

        public void CheckInOnProfile(IPublishingProfile profile, bool recursive)
        {
            throw new NotImplementedException();
        }

        public int CheckOut(bool recursive)
        {
            throw new NotImplementedException();
        }

        public void UndoCheckOut(bool recursive)
        {
            throw new NotImplementedException();
        }

        public IPage CreateChildPage(string name, ISchema schema, bool insertAtBeginning = false)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChildPage(IPage child)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUserGroup> SecurityGroups(EnumNodeLevelPermission security, bool exactLevelMatch = false)
        {
            throw new NotImplementedException();
        }

        public void SetSecurityGroup(EnumNodeLevelPermission security, IUserGroup @group)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSecurityGroup(IUserGroup @group)
        {
            throw new NotImplementedException();
        }

        public EnumNodeLevelPermission SecurityLevel(IUser user = null)
        {
            throw new NotImplementedException();
        }

        public int SecurityGroupsCount()
        {
            throw new NotImplementedException();
        }

        public bool ClearSecurity()
        {
            throw new NotImplementedException();
        }

        public bool AllowUser(IUser user, EnumNodeLevelPermission security)
        {
            throw new NotImplementedException();
        }

        public IAttribute GetSystemAttribute(EnumSystemAttributes attrID)
        {
            throw new NotImplementedException();
        }

        public void SetSystemAttribute(EnumSystemAttributes attrID, IAttribute newVal)
        {
            throw new NotImplementedException();
        }

        public void SetSystemAttribute(EnumSystemAttributes attrID, string newVal)
        {
            throw new NotImplementedException();
        }

        public void AssignUser(IUser user, string comment)
        {
            throw new NotImplementedException();
        }

        public void AssignGroup(IUserGroup @group, string comment)
        {
            throw new NotImplementedException();
        }

        public void ClaimPageForCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IWorkflow AddToWorkflow(IWorkflowDefinition workflowDefinition, IUser assignToUser)
        {
            throw new NotImplementedException();
        }

        public IWorkflow AddToWorkflowDefault(IWorkflowDefinition workflowDefinition)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromWorkflow()
        {
            throw new NotImplementedException();
        }

        public void Republish(IPublishingTarget pubTarget, bool recursive, bool incremental)
        {
            throw new NotImplementedException();
        }

        public void Unassign(string comment)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildren()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPageVersion> Versions(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public int VersionsCount()
        {
            throw new NotImplementedException();
        }

        public IPageVersion Version(int versionNumber)
        {
            throw new NotImplementedException();
        }

        public void RevertToVersion(IPageVersion version)
        {
            throw new NotImplementedException();
        }

        public bool IsValidRuleTarget(IPageCreationRule rule)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPageCreationRuleEntryForPage> PageCreationRules()
        {
            throw new NotImplementedException();
        }

        public int PageCreationRulesCount()
        {
            throw new NotImplementedException();
        }

        public void SetPageCreationRule(IPageCreationRule rule, bool dontIncludeDescendants)
        {
            throw new NotImplementedException();
        }

        public bool RemovePageCreationRule(IPageCreationRule rule)
        {
            throw new NotImplementedException();
        }

        public bool ClearRules()
        {
            throw new NotImplementedException();
        }

        public void SendGroupNotificationMail(IUserGroup @group, IUser assignToUser, string comment, IWorkflow workflow, ITransition transition)
        {
            throw new NotImplementedException();
        }

        public void SendWorkFlowNotificationMail(IWorkflow workflow, ITransition transition, IUser assignToUser, string emailRecipients, string comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICategoryNode> AssociatedCategories(out int count, int pageSize = -1, int startIndex = -1)
        {
            throw new NotImplementedException();
        }

        public int AssociatedCategoriesCount()
        {
            throw new NotImplementedException();
        }

        public void AddCategories(IEnumerable<ICategoryNode> cat)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCategory(ICategoryNode cat)
        {
            throw new NotImplementedException();
        }

        public XElement SerializeCheckedInVersion()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IContentUnitsInstantiability ContentUnitsInstantiability()
        {
            throw new NotImplementedException();
        }

        public bool CanMarkForPublish { get; set; }
        public IHierarchyId Hierarchy { get; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsComponent { get; }
        public bool CheckedOut { get; }
        public bool Removed { get; }
        public bool NowSyncing { get; }
        public bool JustSynced { get; }
        public int Icon { get; set; }
        public ISchema Schema { get; }
        public ISchema CreationSchema { get; }

        ISchemaVersion IPageVersion.SchemaVersion
        {
            get { throw new NotImplementedException(); }
        }

        public IPagePresentationCollection Presentations { get; set; }
        public string SchemaName { get; }
        public string ViewName { get; set; }
        public string Layout { get; set; }

        #pragma warning disable 0067
        public event EventHandler<EntityEventArgs<IEntity>> BeforeEntitySave;
        public event EventHandler<EntityEventArgs<IEntity>> AfterEntitySave;
        public event EventHandler<PageRecursiveEventArgs> BeforePageCheckOut;
        public event EventHandler<PageRecursiveEventArgs> AfterPageCheckOut;
        public event EventHandler<PageRecursiveEventArgs> BeforePageCheckIn;
        public event EventHandler<PageRecursiveEventArgs> AfterPageCheckIn;
        public event EventHandler<PageRecursiveEventArgs> BeforePageUndoCheckOut;
        public event EventHandler<PageRecursiveEventArgs> AfterPageUndoCheckOut;
        public event EventHandler<EntityEventArgs<IPage>> BeforePageRollback;
        public event EventHandler<EntityEventArgs<IPage>> AfterPageRollback;
        public event EventHandler<PageRenameEventArgs> PageRenamed;
        public event EventHandler<EntityEventArgs<IPage>> BeforePageAssign;
        public event EventHandler<EntityEventArgs<IPage>> AfterPageAssign;
        public event EventHandler<PageMarkForPublishEventArgs> BeforePageMarkForPublishChange;
        public event EventHandler<PageMarkForPublishEventArgs> AfterPageMarkForPublishChange;
        #pragma warning restore 0067
    }
}
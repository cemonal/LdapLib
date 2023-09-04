# LdapLib

LdapLib is a .NET library that provides convenient and efficient access to LDAP (Lightweight Directory Access Protocol) services. It simplifies the interaction with LDAP servers and helps manage users, computers, and groups in Active Directory environments.

## Compatibility

LdapLib is compatible with .NET Framework 4.8.1.

## Features

- Easy setup and configuration using app/web.config files.
- Seamless integration with Active Directory using PrincipalContext and DirectoryEntry.
- Services for managing LDAP users, computers, and groups.
- Rich search capabilities with customizable search parameters.
- Password management, account unlocking, and more.

## Installation

To use LdapLib in your project, you can download and reference the compiled library, or you can add it using NuGet:

```powershell
Install-Package LdapLib
```

## Configuration

To use LdapLib in your project, you need to configure your `app.config` or `web.config` file with the necessary settings for connecting to your LDAP server.

### LdapSettings

The `LdapSettings` section contains the basic connection settings for your LDAP server. Here's an example configuration:

```xml
<LdapConfiguration>
    <LdapSettings>
        <add server="ldap://your-ldap-server-url" domain="" username="your-username" password="your-password" authenticationType="Secure" scope="Subtree"/>
    </LdapSettings>
    <!-- Add more LdapContainerSettings if needed -->
</LdapConfiguration>
```
- **server**: The URL of your LDAP server.
- **domain**: The domain of the user account (if required).
- **username**: The username for authentication.
- **password**: The password for authentication.
- **authenticationType**: The type of authentication to use (e.g., "Secure" or "Basic").
- **scope**: The scope of the search (e.g., "Subtree" for searching the entire tree).

### LdapContainerSettings

If your LDAP server has multiple containers, you can define them in the LdapContainerSettings section. Here's an example:

```xml
<LdapContainerSettings>
    <add key="users" type="User" path="CN=Users,DC=example,DC=com"/>
    <add key="computers" type="Computer" path="CN=Computers,DC=example,DC=com"/>
</LdapContainerSettings>
```
- **key**: A unique identifier for the container.
- **type**: The type of container (e.g., "User", "Computer", etc.).
- **path**: The LDAP path of the container.

Make sure to adjust the values according to your LDAP server's configuration.

## Services

### LdapUserService
Provides functionality related to managing user accounts in the LDAP directory.

- **ChangePassword(samAccountName, oldPassword, newPassword)**: Changes the account password from the old password to the new password.
- **ExpirePasswordNow(samAccountName)**: Expires the password for an account, requiring the user to change it at the next logon.
- **GetAuthorizationGroups(samAccountName)**: Retrieves the authorization groups of a user.
- **FindByBadPasswordAttempt(time, type)**: Finds users with incorrect password attempts within a specified time range.
- **FindByExpirationTime(time, type)**: Finds users with account expiration times in a specified time range.
- **FindByLockoutTime(time, type)**: Finds users with account lockout times in a specified time range.
- **FindByLogonTime(time, type)**: Finds users with account logon times in a specified time range.
- **FindByPasswordSetTime(time, type)**: Finds users who set their passwords within a specified time range.
- **IsAccountLockedOut(samAccountName)**: Checks if a user account is currently locked out.
- **UnlockAccount(samAccountName)**: Unlocks a locked user account.
- **RefreshExpiredPassword(samAccountName)**: Refreshes an expired password.

### LdapComputerService
Provides functionality related to managing computer accounts in the LDAP directory.

- **FindByBadPasswordAttempt(time, type)**: Finds computers with bad password attempts within a specified time range.
- **FindByExpirationTime(time, type)**: Finds computers with account expiration times in a specified time range.

### LdapGroupService
Provides functionality related to managing group accounts in the LDAP directory.

- **GetMembers(samAccountName, recursive)**: Retrieves the members of a group, optionally searching recursively.

## Common Methods (Inherited from Base Repository)

These methods are common to all repositories and are inherited from the base repository.

### Methods

- **void Delete(string samAccountName)**: Deletes the principal object by sAM account name from the store.
- **void Delete(IdentityType identityType, string identityValue)**: Deletes the principal object from the store.
- **SearchResultCollection GetAll()**: Gets all principal objects from the store.
- **SearchResultCollection GetAll(string[] propertiesToLoad)**: Gets all principal objects from the store with specified properties to load.
- **SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption)**: Gets all principal objects from the store with specified properties to load and sort option.
- **PrincipalSearchResult\<Principal\> GetGroups(string samAccountName)**: Returns a collection of group objects that specify the groups of which the current principal is a member.
- **PrincipalSearchResult\<Principal\> GetGroups(IdentityType identityType, string identityValue)**: Returns a collection of group objects that specify the groups of which the current principal is a member.
- **SearchResult Find(LdapFindParameters parameters)**: Finds a principal object in the store based on the provided parameters.
- **T FindByIdentity(IdentityType identityType, string identityValue)**: Finds a principal object in the store by identity.
- **bool IsMemberOf(IdentityType identityType, string identityValue, IdentityType groupIdentityType, string groupIdentityValue)**: Checks if a principal object is a member of a specific group.
- **PrincipalSearchResult\<T\> PrincipalSearch()**: Performs a principal search.
- **SearchResultCollection Search(LdapSearchParameters parameters)**: Performs a search for principal objects based on the provided parameters.
- **T Create(T principal, string password)**: Creates a new principal object in the store.
- **void Update(T principal)**: Updates a principal object in the store.

## Example Usage

### Connecting to LDAP

To establish a connection to the LDAP server, you can use the `LdapConnection` class. You need to provide the following configuration settings:

- **server**: The URL of your LDAP server.
- **domain**: The domain of the user account (if required).
- **username**: The username for authentication.
- **password**: The password for authentication.
- **authenticationType**: The type of authentication to use (e.g., "Secure" or "Basic").
- **scope**: The scope of the search (e.g., "Subtree" for searching the entire tree).

```csharp
using LdapLib;
using LdapLib.Config;

// Load configuration settings from app.config or web.config
var settings = LdapConfigurationsHelper.GetSettings();

using (var connection = new LdapConnection(settings.Server, settings.Container, settings.Domain, settings.Username, settings.Password, settings.AuthenticationType))
{
    // Now you have an active connection to the LDAP server
    // Perform operations using connection.Context or connection.DirectoryEntry
}
```

### Managing User Accounts

```csharp
using LdapLib;
using LdapLib.Config;
using LdapLib.Services;

// Load configuration settings from app.config or web.config
var settings = LdapConfigurationsHelper.GetSettings();

using (var connection = new LdapConnection(settings.Server, settings.Container, settings.Domain, settings.Username, settings.Password, settings.AuthenticationType))
{
    var userService = new LdapUserService(connection);

    // Change user password
    userService.ChangePassword("username", "oldPassword", "newPassword");

    // Expire user password
    userService.ExpirePasswordNow("username");

    // Get user's authorization groups
    var groups = userService.GetAuthorizationGroups("username");

    // ... and more operations
}
```

### Managing Group Accounts

```csharp
using LdapLib;
using LdapLib.Config;
using LdapLib.Services;

// Load configuration settings from app.config or web.config
var settings = LdapConfigurationsHelper.GetSettings();

using (var connection = new LdapConnection(settings.Server, settings.Container, settings.Domain, settings.Username, settings.Password, settings.AuthenticationType))
{
    var groupService = new LdapGroupService(connection);

    // Get members of a group
    var groupMembers = groupService.GetMembers("groupname");

    // ... and more operations
}
```

### Managing Computer Accounts

```csharp
using LdapLib;
using LdapLib.Config;
using LdapLib.Services;

// Load configuration settings from app.config or web.config
var settings = LdapConfigurationsHelper.GetSettings();

using (var connection = new LdapConnection(settings.Server, settings.Container, settings.Domain, settings.Username, settings.Password, settings.AuthenticationType))
{
    var computerService = new LdapComputerService(connection);

    // Find computers with bad password attempts
    var computersWithBadAttempts = computerService.FindByBadPasswordAttempt(DateTime.Now, MatchType.Equals);

    // ... and more operations
}
```

## Contributing

Contributions are welcome! Feel free to submit issues, pull requests, or feedback in the GitHub repository.

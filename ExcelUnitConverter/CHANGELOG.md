## [Unreleased]

## [0.1.0] - 2017-02-21
This is the initial public release of the add-in.

### Changes
 - Move unit database to SQLite database and embed as a resource.  This removes the .txt files which were previously embedded.

### Added
 - Initialization now creates a folder in AppData in order to store the user unit database
 - sqlite-net-pcl NuGet packages added to support SQLite ORM operations
 - Created a BaseUnitDef which is used to back the base units (need to update references to use this class instead of the string)
 - Creates a form to review the current units
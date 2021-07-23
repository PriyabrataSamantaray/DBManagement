@ECHO OFF
ECHO "Started"
SET workspacePath=%1
SET createDBPath=%2
ECHO %workspacePath%
ECHO %createDBPath%

GOTO EXECUTECREATEDB

:EXECUTECREATEDB
IF EXIST %workspacePath%\myTeam\sql\sql (	
	IF EXIST %createDBPath% (		
		GOTO CALLCREATEDB
	)ELSE ( 
		ECHO "Create DB Folder Not Found" 
		GOTO END )
)ELSE ( 
ECHO "Workspace Folder Not Found" 
GOTO CLONECODE )

:PULLCODEFROMBB


:CALLCREATEDB
CALL %createDBPath%\createdb.exe MININT-SH1CLGC myTeam gfpPrm sql myTeam_3.6.schema	
GOTO END

:CLONECODE
setx devhome "%workspacePath%"
git clone ssh://git@amatbb.amat.com:7999/esi/myteam.git
GOT EXECUTECREATEDB

:end
echo End of batch program.
@echo off
chcp 65001 > nul
cd /d "%~dp0"

sqlcmd -S ".\SQLEXPRESS" -E -i "00_Run_All.sql" -b -f 65001

pause
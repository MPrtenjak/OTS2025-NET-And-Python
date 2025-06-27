@echo off
setlocal

set PYTHON_FOLDER=Python312
set PYTHON_ZIP=python-embed.zip
set PYTHON_URL=https://github.com/astral-sh/python-build-standalone/releases/download/20250626/cpython-3.12.11+20250626-x86_64-pc-windows-msvc-pgo-full.tar.zst
set VENV_NAME=virtual-env

echo Prenos Python paketa...
curl -L -o %PYTHON_ZIP% %PYTHON_URL%

echo Razsirjanje v mapo %PYTHON_FOLDER%...
tar -xf %PYTHON_ZIP%
ren python %PYTHON_FOLDER%
del %PYTHON_ZIP%

echo Instalacija virtualnega okolja...
%PYTHON_FOLDER%\install\python.exe -m pip install virtualenv
 
echo Ustvarjanje virtualnega okolja...
%PYTHON_FOLDER%\install\python.exe -m virtualenv  %VENV_NAME%

echo Aktivacija virtualnega okolja...
call %VENV_NAME%\Scripts\activate.bat

echo Namestitev PyPDF2...
pip install PyPDF2

echo Koncano! Python je v mapi '%PYTHON_FOLDER%', virtualno okolje pa v '%VENV_NAME%'.

endlocal
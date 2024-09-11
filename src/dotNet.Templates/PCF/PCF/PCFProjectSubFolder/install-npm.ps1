$origPWD = $PWD

cd $PSScriptRoot

echo " running npm install"

npm install

remove-item install-npm.ps1

cd $origPWD



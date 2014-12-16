#/bin/bash

git submodule foreach git pull
git pull
xbuild /p:Configuration=Release GraphDevroom2015.sln

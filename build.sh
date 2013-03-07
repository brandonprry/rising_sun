#!/bin/sh

if [ `whoami` != "root" ]
then
  echo "I need to be root."
  exit 255
fi

xbuild autoassess_service.sln

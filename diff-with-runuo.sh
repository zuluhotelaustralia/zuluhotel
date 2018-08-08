#!/bin/sh

usage()
{
    echo "Usage: $0 [zuluhotel-directory] [runuo-directory]"
}

if [ -z "$1" -o -z "$2" ]; then
    usage
    exit 0
fi

zuluhotel=$1
runuo=$2

CORE="$zuluhotel/core.patch"
SCRIPTS="$zuluhotel/scripts.patch"

find $zuluhotel/Server $runuo/Server \( -type d -name 'Scripts' -prune \) -o -type f -name '*.cs' -print | sed -e "s,^$zuluhotel,," -e "s,^$runuo,," | sed -e "s,^/,," | sort | uniq | xargs -d'\n' -n1 -I% diff --minimal --ignore-all-space -Nua $runuo/% $zuluhotel/% > $CORE

find $zuluhotel/Server/Scripts $runuo/Scripts -type f -name '*.cs' -print | sed -e "s,^$zuluhotel/Server,," -e "s,^$runuo,," | sed -e "s,^/,," | sort | uniq | xargs -d'\n' -n1 -I% diff --minimal --ignore-all-space -Nua $runuo/% $zuluhotel/Server/% > $SCRIPTS

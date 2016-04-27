#!/bin/sh
# This wrapper makes it possible to call the same 
# command in both Windows' CMD shell and Bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
mono "$DIR/razor-cli.exe" "$@"

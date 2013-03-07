#!/bin/bash

find . | grep \.mdb$ | xargs rm
find . | grep \.pidb$ | xargs rm

# Enki.Common

Simple and common functions shared for Enki products.

## Source Link to lib

On add source link to lib, the user can debug lib without attach source code.

* See how: https://carlos.mendible.com/2018/08/25/adding-sourcelink-to-your-net-core-library/?utm_source=dlvr.it&utm_medium=twitter

## Public Version

### Prerelease version

On some branch, run the follow:

`make build && make run-test && make pack && make push-pack`

This will generate an pre-release version with follow structure {version}.{pack}.{version}-{prerelease-sufix}

### Release version

On Master, run the follow:

`make build && make run-test && make pack && make push-pack`

This will generate an pre-release version with follow structure {version}.{pack}.{version}

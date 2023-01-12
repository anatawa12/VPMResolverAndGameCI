#!/bin/sh

set -eu

resolve() {
  curl -sL "$2" > out.zip
  mkdir -p "Packages/$1"
 pushd "Packages/$1"
  unzip ../../out.zip
 popd
  rm out.zip
}

# "com.vrchat.clientsim": "1.2.2"
resolve com.vrchat.clientsim "https://vpm.directus.app/assets/2ac72870-1395-4a17-ad54-55354c3e3363?download"
# "com.vrchat.worlds": "3.1.10"
resolve com.vrchat.worlds "https://vpm.directus.app/assets/0da87612-a243-4edb-81b5-ab3486a2d028?download"
# "com.vrchat.base": "3.1.10"
resolve com.vrchat.base "https://vpm.directus.app/assets/34791633-d353-45e0-acdd-d60e4e5d5af8?download"
# "com.vrchat.core.vpm-resolver": "0.1.17"
# should already installed

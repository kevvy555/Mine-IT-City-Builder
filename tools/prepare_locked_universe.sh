#!/usr/bin/env bash
set -euo pipefail

destination="${1:-$RUNNER_TEMP/mineit-universe}"
output="${2:-MineITCityBuilder/Assets/Game/Generated/Resources/Canon}"

readarray -t lock_values < <(python3 - <<'PY'
import json
lock = json.load(open("config/universe.lock.json", encoding="utf-8"))
print(lock["repository"])
print(lock["commit"])
PY
)

repository="${lock_values[0]}"
commit="${lock_values[1]}"

rm -rf "$destination"
mkdir -p "$destination"
git -C "$destination" init --quiet
git -C "$destination" remote add origin "https://github.com/$repository.git"
git -C "$destination" fetch --quiet --depth=1 origin "$commit"
git -C "$destination" checkout --quiet --detach FETCH_HEAD

actual="$(git -C "$destination" rev-parse HEAD)"
if [[ "$actual" != "$commit" ]]; then
  echo "::error::Universe checkout mismatch: expected $commit, got $actual"
  exit 1
fi

python3 tools/universe_import.py --source "$destination" --output "$output"

### Init Git
```bash

```

### Remove Git History
```bash
git checkout --orphan z
# git reset --hard # <- Becare full with this

git add .
git commit -m 'INIT'
# git push --set-upstream origin z
git branch -D main
git branch -m main
git push -f origin main
```
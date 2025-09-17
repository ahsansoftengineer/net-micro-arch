### Init Git
```bash

```

### Remove Git History
```bash
git checkout --orphan z
git add .
git commit -m 'INIT'
# git push --set-upstream origin z
git branch -D main
git branch -m main
git push -f origin main
```
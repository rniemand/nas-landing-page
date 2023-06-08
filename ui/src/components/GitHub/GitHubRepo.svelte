<style>
  .repo {
    border-bottom: 1px solid #353535;
    margin-bottom: 6px;
    padding: 6px;
    display: flex;
  }
  .repo .name { flex: auto; font-weight: bold; color: rgb(14 221 67); }
  .repo .link { width: 50px; }
  .repo .branch { width: 50px; text-align: center; }
  .repo .size { width: 95px; color: rgb(255, 244, 149); text-align: center; }
  .repo .last-push { width: 90px; text-align: center; }
  .repo .age { width: 90px; text-align: center; }
  .repo:hover { background-color: #353535; }
  a { color: rgb(44, 145, 235); text-decoration: none; font-weight: bold; cursor: pointer; }
  a:hover { color: rgb(240, 18, 18); }
</style>

<script lang="ts">
  import type { GitHubRepoDto } from "../../nlp-api";
  import { humanFileSize } from "../../utils/data-utils";
  import { fmtDateShort, humanTimeDiff } from "../../utils/date-utils";

  export let repo: GitHubRepoDto;
</script>

<div class="repo">
  <div class="name">{repo.fullName}</div>
  <div class="link"><a href={`https://github.com/${repo.fullName}`} target="_blank">Open</a></div>
  <div class="branch">{repo.defaultBranch}</div>
  <div class="size">{humanFileSize(repo.repoSize * 1024)}</div>
  <div class="last-push">{fmtDateShort(repo.updatedAt)}</div>
  <div class="age">{humanTimeDiff(repo.updatedAt)}</div>
</div>
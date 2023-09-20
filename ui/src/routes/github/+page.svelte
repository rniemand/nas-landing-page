<style>
  .controls { padding: 3px; text-align: right; }
  button {
    border: 1px solid rgb(94, 93, 93);
    padding: 4px 6px;
    background-color: rgb(73, 73, 73);
    color: #fff;
    border-radius: 4px;
    cursor: pointer;
  }
  .refresh { background-color: rgb(26, 71, 5); }
</style>

<script lang="ts">
  import GitHubRepoList from "../../components/GitHub/GitHubRepoList.svelte";
  import { GitHubClient, GitHubRepoDto } from "../../nlp-api";

  let loading = true;
  let repos: GitHubRepoDto[] = [];

  const refreshRepos = () => {
    new GitHubClient().listRepos().then((_repos: GitHubRepoDto[]) => {
      repos = _repos || [];
      loading = false;
    });
  };

  const runIndexing = () => {
    new GitHubClient().triggerRepoIndex().then((_response: string) => {
      console.log('response', _response);
    });
  };

  (async () => { refreshRepos(); })();
</script>

<div class="alert alert-warning" role="alert">
  This is a work in progress!
</div>

<div>
  <div class="controls">
    <button on:click={runIndexing}>Sync Repos</button>
    <button class="refresh" on:click={refreshRepos}>Refresh Repos</button>
  </div>
  {#if loading}
    Loading...
  {:else}
    <GitHubRepoList {repos} />
  {/if}
</div>
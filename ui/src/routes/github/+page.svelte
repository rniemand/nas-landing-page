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

<div class="alert alert-warning my-3">
  <svg xmlns="http://www.w3.org/2000/svg" class="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" /></svg>
  <span>This is a work in progress!</span>
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
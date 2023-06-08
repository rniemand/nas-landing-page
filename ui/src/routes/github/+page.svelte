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

<div>
  <div>
    <button on:click={runIndexing}>Sync Repos</button>
  </div>
  {#if loading}
    Loading...
  {:else}
    <GitHubRepoList {repos} />
  {/if}
</div>
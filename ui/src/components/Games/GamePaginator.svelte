<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";

  interface PageInfo {
    pageNumber: number;
    active: boolean;
  }
  
  export let games: BasicGameInfoDto[] = [];
  export let pageSize: number = 10;
  export let onPaginationChanged: (games: BasicGameInfoDto[]) => void;
  let showPaginator: boolean = false;
  let pages: PageInfo[] = [];

  const gamesChanged = (_games: BasicGameInfoDto[]) => {
    showPaginator = _games.length > pageSize;
    pages = [];
    for(let i = 1; i < Math.ceil(_games.length / pageSize)+1; i++) {
      pages.push({
        pageNumber: i,
        active: i === 1,
      });
    }
    setActivePage(1);
  };

  const setActivePage = (pageNumber: number) => {
    for(const page of pages) {
      page.active = pageNumber === page.pageNumber;
    }
    pages = pages;

    let startIdx = (pageNumber - 1) * pageSize;
    let endIdx = startIdx + pageSize;
    onPaginationChanged(games.slice(startIdx, endIdx));
  };

  $: gamesChanged(games);
</script>

{#if showPaginator}
  <div class="flex">
    <h1 class="flex-auto text-xl">{games.length} Game(s)</h1>
    <div class="join mb-3">
      {#each pages as page}
        <button class="join-item btn" class:btn-active={page.active} on:click={() => setActivePage(page.pageNumber)}>
          {page.pageNumber}
        </button>
      {/each}
    </div>
  </div>
{/if}
<style>
  h2 { flex: auto; }
  .refresh { cursor: pointer; }
</style>

<script lang="ts">
  import { BasicGameInfoDto, GamesClient, GamePlatformDto } from "../../nlp-api";
  import GameCard from "./GameCard.svelte";
  import GameSearch from "./GameSearch.svelte";

  export let selectedPlatform: GamePlatformDto | undefined;
  export let triggerAction: (action: string, game: BasicGameInfoDto | undefined) => void;
  export const refresh = () => refreshGames(selectedPlatform);
  let games: BasicGameInfoDto[] = [];
  let filteredGames: BasicGameInfoDto[] = [];
  let searchTerm: string = '';
  let loading = true;

  const refreshGames = (platform: GamePlatformDto | undefined) => {
    games = [];
    if(!platform) { loading = false; return; }
    searchTerm = '';
    new GamesClient().getPlatformGames(platform?.platformID)
      .then((_games: BasicGameInfoDto[]) => { games = _games; })
      .finally(() => {
        searchTermChangedHandler(searchTerm);
        loading = false;
      });
  };

  const searchTermChangedHandler = (term: string) => {
    let safeTrem = (term || '').toLowerCase();
    filteredGames = games.filter(x => x.searchTerm.indexOf(safeTrem) !== -1);
  };

  const onAddGameClicked = () => triggerAction('add-game', undefined);

  $: refreshGames(selectedPlatform);
</script>

<div>
  {#if loading}
    Loading...
  {:else}
    <div class="d-flex my-2">
      <h2>
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <i class="bi bi-arrow-clockwise refresh" on:click={() => refreshGames(selectedPlatform)}></i>
        Showing {games.length} game(s)
      </h2>
      <GameSearch searchTermChanged={searchTermChangedHandler} onAddGame={onAddGameClicked} bind:term={searchTerm} />
    </div>
    <div class="row row-cols-6 g-4">
      {#each filteredGames as game (game.gameID)}<GameCard {game} {triggerAction} />{/each}
    </div>
  {/if}
</div>
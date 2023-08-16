<script lang="ts">
	import { BasicGameInfoDto, GamesClient, type GamePlatformDto, GameLocationDto } from "../../nlp-api";
	import GameLocationSelector from "./GameLocationSelector.svelte";

    export let platform: GamePlatformDto | undefined;
    export let onGameAdded: () => void;
    let game: BasicGameInfoDto = new BasicGameInfoDto();
    let formValid = false;

    const resetGame = (_platform: GamePlatformDto | undefined) => {
        game = new BasicGameInfoDto({
            gameCaseLocation: '',
            gameID: 0,
            gameName: '',
            gamePrice: 0,
            gameRating: 0,
            gameSold: false,
            hasGameBox: false,
            hasProtection: false,
            hasReceipt: false,
            imagePath: '',
            locationID: 0,
            locationName: '',
            platformID: _platform?.platformID || 0,
            platformName: _platform?.platformName || '',
            receiptID: 0,
            receiptName: '',
            receiptNumber: '',
            receiptScanned: false,
            searchTerm: '',
            store: '',
            receiptDate: new Date(),
        });
    };

    const valueChanged = () => {
        formValid = false;
        if((game?.gameName?.length || 0) < 2) return;
        formValid = true;
    };

    const addGame = () => {
        new GamesClient().addGame(game)
            .then((success: boolean) => {
                if(!success) {
                    alert('Failed to add game!');
                    return;
                }
                game = new BasicGameInfoDto();
                resetGame(platform);
                onGameAdded();
            });
    };

    const onGameLocationChanged = (location: GameLocationDto | undefined) => {
        game.locationID = location?.locationID || 0;
        game.locationName = location?.locationName || '';
    };

    $: resetGame(platform);
</script>

{#if platform}
  <form class="row g-3">
    <div class="col-md-6">
      <label for="gameName" class="form-label">Name</label>
      <input type="text" id="gameName" class="form-control" bind:value={game.gameName} on:keyup={valueChanged}>
    </div>
    <div class="col-md-6">
      <label for="gameLocation" class="form-label">Location</label>
      <GameLocationSelector {platform} onLocationChanged={onGameLocationChanged} />
    </div>
    <div class="col-md-6">
      <label for="caseLocation" class="form-label">Case Location</label>
      <input type="text" id="caseLocation" class="form-control" bind:value={game.gameCaseLocation} on:keyup={valueChanged}>
    </div>
    <div class="col-md-3">
      <label for="gamePrice" class="form-label">Price</label>
      <input type="number" id="gamePrice" class="form-control" bind:value={game.gamePrice} on:keyup={valueChanged}>
    </div>
    <div class="col-3">
      <label for="rating" class="form-label">Rating</label>
      <input type="number" min="0" class="form-control" max="10" id="rating" bind:value={game.gameRating} on:keyup={valueChanged}>
    </div>
    <div class="d-flex">
      <div class="col-6">
        <div class="form-check form-switch form-check-inline">
          <input type="checkbox" class="form-check-input" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged}>
          <label class="form-check-label" for="hasBox">Has Box</label>
        </div>
      </div>
      <div class="col-6">
        <div class="form-check form-switch form-check-inline">
          <input type="checkbox" class="form-check-input" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged}>
          <label class="form-check-label" for="hasProtection">Has Protection</label>
        </div>
      </div>
    </div>
    <button disabled={!formValid} class="btn btn-success" on:click={addGame}>Add Game</button>
  </form>
{/if}
<style>
    h2 {
        font-size: 2em;
        text-align: center;
        margin-bottom: 12px;
    }
    .form .row {
        display: flex;
        justify-content: space-evenly;
        margin-bottom: 12px;
    }
    .form .field label {
        font-weight: bold;
        padding-top: 2px;
        margin-right: 4px;
    }
    .form .field {
        flex: auto;
        display: flex;
    }
    .form .field input {
        flex: auto;
        margin-right: 6px;
        padding: 2px;
        border-radius: 4px;
        border: 1px solid #b9b5b5;
    }
</style>

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
    <div>
        <div class="form">
            <div class="row">
              <div class="field">
                <label for="location">Case</label>
                <input type="text" id="location" bind:value={game.gameCaseLocation} on:keyup={valueChanged}>
              </div>
              <div class="field">
                <label for="name">Name</label>
                <input type="text" id="name" bind:value={game.gameName} on:keyup={valueChanged}>
              </div>
            </div>
          
            <div class="row">
              <div class="field">
                <label for="price">Price</label>
                <input type="number" id="price" bind:value={game.gamePrice} on:keyup={valueChanged}>
              </div>
              <div class="field">
                <label for="rating">Rating</label>
                <input type="number" max="10" min="0" id="rating" bind:value={game.gameRating} on:keyup={valueChanged}>
              </div>
              <div class="field">
                <label for="location">Location</label>
                <GameLocationSelector {platform} onLocationChanged={onGameLocationChanged} />
              </div>
            </div>
          
            <div class="row">
                <div class="field">
                    <label for="hasBox">Has Box</label>
                    <input type="checkbox" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged}>
                  </div>
                  <div class="field">
                    <label for="hasProtection">Has Protection</label>
                    <input type="checkbox" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged}>
                  </div>
            </div>
          
            <div class="row">
              <button disabled={!formValid} on:click={addGame}>Add Game</button>
            </div>
          </div>
    </div>
{/if}
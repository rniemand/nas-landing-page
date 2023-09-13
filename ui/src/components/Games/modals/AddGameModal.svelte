<script lang="ts">
  import { BasicGameInfoDto, GamesClient, type GamePlatformDto, GameLocationDto } from "../../../nlp-api";
  import GameLocationSelector from "../GameLocationSelector.svelte";

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
  <form>
    <div class="form-control w-full">
      <label class="label" for="gameName">
        <span class="label-text">Name</span>
      </label>
      <input type="text" id="gameName" class="input input-bordered w-full" bind:value={game.gameName} on:keyup={valueChanged} />
    </div>
    <div class="form-control w-full">
      <label class="label" for="gameLocation">
        <span class="label-text">Location</span>
      </label>
      <GameLocationSelector {platform} onLocationChanged={onGameLocationChanged} />
    </div>
    <div class="flex">
      <div class="mr-1">
        <label class="label" for="caseLocation">
          <span class="label-text">Case Location</span>
        </label>
        <input type="text" id="caseLocation" class="input input-bordered w-full" bind:value={game.gameCaseLocation} on:keyup={valueChanged} />
      </div>
      <div class="mr-1">
        <label class="label" for="gamePrice">
          <span class="label-text">Price</span>
        </label>
        <input type="number" id="gamePrice" class="input input-bordered w-full" bind:value={game.gamePrice} on:keyup={valueChanged} />
      </div>
      <div>
        <label class="label" for="gameRating">
          <span class="label-text">Rating</span>
        </label>
        <input type="number" min="0" max="10" id="gameRating" class="input input-bordered w-full" bind:value={game.gameRating} on:keyup={valueChanged} />
      </div>
    </div>
    <div class="flex">
      <div class="form-control flex-1">
        <label class="label cursor-pointer">
          <span class="label-text">Has Box</span> 
          <input type="checkbox" class="toggle" id="hasBox" bind:checked={game.hasGameBox} on:change={valueChanged} />
        </label>
      </div>
      <div class="form-control flex-1">
        <label class="label cursor-pointer">
          <span class="label-text">Has Protection</span> 
          <input type="checkbox" class="toggle" id="hasProtection" bind:checked={game.hasProtection} on:change={valueChanged} />
        </label>
      </div>
    </div>
    <button disabled={!formValid} class="btn btn-primary btn-md w-full mt-2" on:click={addGame}>Add Game</button>
  </form>
{/if}
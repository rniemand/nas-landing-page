<script lang="ts">
  import { BasicGameInfoDto, GamePlatformsClient, GamePlatformDto } from "../../nlp-api";
  import Modal from "../Modal.svelte";
	import GameAddModal from "./GameAddModal.svelte";
	import GameInfoModal from "./GameInfoModal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
	import GameSetConsoleModal from "./GameSetConsoleModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: GamePlatformDto[];
  let selectedPlatform: GamePlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let receiptModal: Modal;
  let gameModal: Modal;
  let setConsoleModal: Modal;
  let addGameModal: Modal;
  let gameList: PlatformGameList;

  const platformSelected = (platform: GamePlatformDto) => selectedPlatform = platform;
  const onModalClosed = () => gameList.refresh();

  const triggerActionHandler = (action: string, game: BasicGameInfoDto | undefined) => {
    if(game) selectedGame = game;
    if(action === 'receipt') { receiptModal.open(onModalClosed); }
    if(action === 'game-info') { gameModal.open(onModalClosed); }
    if(action === 'set-console') { setConsoleModal.open(onModalClosed); }
    if(action === 'add-game') { addGameModal.open(onModalClosed); }
  };
  
  (async () => {
    platforms = await new GamePlatformsClient().getAll();
    selectedPlatform = platforms ? platforms[0] : undefined;
    loading = false;
  })();
</script>

<div class="wrapper">
  {#if loading}
    Loading...
  {:else}
    <Modal bind:this={receiptModal}><GameReceiptModal game={selectedGame} /></Modal>
    <Modal bind:this={gameModal}><GameInfoModal game={selectedGame} /></Modal>
    <Modal bind:this={addGameModal}><GameAddModal platform={selectedPlatform} onGameAdded={() => addGameModal.close()} /></Modal>
    <Modal bind:this={setConsoleModal}><GameSetConsoleModal game={selectedGame} onLocationSet={() => setConsoleModal.close()} /></Modal>
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={platformSelected} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>
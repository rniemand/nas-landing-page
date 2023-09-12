<script lang="ts">
  import { BasicGameInfoDto, GamePlatformsClient, GamePlatformDto } from "../../nlp-api";
	import AddGameModal from "./AddGameModal.svelte";
	import GameInfoModal from "./GameInfoModal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
	import SetConsoleModal from "./SetConsoleModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";
	import { GameModal } from "./Games";

  let loading = true;
  let platforms: GamePlatformDto[];
  let selectedPlatform: GamePlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let gameList: PlatformGameList;
  let modalType: string = '';
  let modalTitle: string = '';
  let modalDialog: HTMLDialogElement;

  const platformSelected = (platform: GamePlatformDto) => selectedPlatform = platform;
  const onModalClosed = () => gameList.refresh();

  const triggerActionHandler = (action: string, game: BasicGameInfoDto | undefined) => {
    if(game) selectedGame = game;
    modalType = action;
    if(action === GameModal.Receipt) modalTitle = 'Game Receipt';
    if(action === GameModal.GameInfo) modalTitle = 'Game Info';
    if(action === GameModal.SetConsole) modalTitle = 'Set Console';
    if(action === GameModal.AddGame) modalTitle = 'Add Game';
    modalDialog.showModal();
  };

  const closeModal = () => {
    modalDialog.close();
    onModalClosed();
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
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={platformSelected} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>

<dialog class="modal" bind:this={modalDialog}>
  <div class="modal-box">
    <h2 class="font-bold text-lg">{modalTitle}</h2>
    {#if modalType === GameModal.Receipt}<GameReceiptModal game={selectedGame} onReceiptAssociated={closeModal} />{/if}
    {#if modalType === GameModal.GameInfo}<GameInfoModal game={selectedGame} onGameUpdated={closeModal} />{/if}
    {#if modalType === GameModal.AddGame}<AddGameModal platform={selectedPlatform} onGameAdded={closeModal} />{/if}
    {#if modalType === GameModal.SetConsole}<SetConsoleModal game={selectedGame} onLocationSet={closeModal} />{/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>
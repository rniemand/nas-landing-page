<style>
  .modal {
    display: none;
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0,0,0);
    background-color: rgb(56 85 161 / 44%);
  }
  .modal-content { background-color: #fefefe; margin: 15% auto; padding: 20px; border: 1px solid #888; color: #000; }
  .close { color: #aaa; font-size: 28px; font-weight: bold; }
  .controls { text-align: right; }
  .close:hover, .close:focus { color: black; text-decoration: none; cursor: pointer; }
  .open { display: block; }
  .modal-content.small { width: 30%; max-width: 450px; }
  .modal-content.medium { width: 50%; max-width: 800px; }
  .modal-content.large { width: 80%; }
</style>

<script lang="ts">
  let closeCallback: (() => void) | undefined = undefined;
  let visible = false;

  export let size: 'small' | 'medium' | 'large' = 'medium';

  export const open = (onCloseCallback: (() => void) | undefined = undefined) => {
    closeCallback = onCloseCallback;
    visible = true;
  };

  export let close = () => {
    visible = false;
    if(closeCallback) closeCallback();
  };
</script>

<div id="myModal" class="modal" class:open={visible}>
  <div class="modal-content" class:small={size === 'small'} class:medium={size === 'medium'} class:large={size === 'large'}>
    <div class="controls">
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <span class="close" on:click={close}>&times;</span>
    </div>
    <slot />
  </div>
</div>
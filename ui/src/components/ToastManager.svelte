<script lang="ts">
	import { onMount } from 'svelte';
	import { Toast, ToastBody, ToastHeader } from 'sveltestrap';
	import { NlpToastMessage, toastSuccess, toasts } from './ToastManager';
	let toastMessages: NlpToastMessage[] = [];

	onMount(() => {
		return toasts.subscribe((_entries: NlpToastMessage[]) => {
			toastMessages = _entries;
		});
	});
</script>

<div class="toast-container position-fixed bottom-0 end-0 p-3">
	{#each toastMessages as entry}
		<Toast>
			<ToastHeader icon={entry.color} toggle={entry.dismiss}>
				{entry.title}
			</ToastHeader>
			<ToastBody>{entry.body}</ToastBody>
		</Toast>
	{/each}
</div>

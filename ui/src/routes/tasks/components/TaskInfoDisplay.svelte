<style>
	.description {
		border: 1px solid #3f3f3f;
		background-color: #161616;
		border-radius: 0.5em;
		padding: 0.5em;
	}
</style>

<script lang="ts">
	import { Button, Icon } from 'sveltestrap';
	import type { UserTaskDto } from '../../../nlp-api';

	export let task: UserTaskDto;
	export let onCompleteTask: (task: UserTaskDto) => void;
	export let onEditTask: (task: UserTaskDto) => void;
</script>

{#if task.taskDescription.length > 0}
	<div class="description">
		{task.taskDescription}
	</div>
{/if}

<div class="d-flex my-3">
	<span class="flex-fill">{task.taskCategory}</span>
	<span class="flex-fill text-end">{task.taskSubCategory}</span>
</div>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={() => onEditTask(task)}>
		<Icon name="pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button color="success" class="flex-fill me-1" on:click={() => onCompleteTask(task)}>
		<Icon name="clipboard-check" />
		<span class="d-none d-sm-inline">Complete</span>
	</Button>
</div>

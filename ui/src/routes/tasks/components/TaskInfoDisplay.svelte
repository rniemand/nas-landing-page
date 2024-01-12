<script lang="ts">
	import { Button, Icon } from 'sveltestrap';
	import type { UserTaskDto } from '../../../nlp-api';
	import DetailsContainer from '../../../components/common/DetailsContainer.svelte';
	import DetailsContainerEntry from '../../../components/common/DetailsContainerEntry.svelte';

	export let task: UserTaskDto;
	export let onCompleteTask: (task: UserTaskDto) => void;
	export let onEditTask: (task: UserTaskDto) => void;
</script>

{#if task.taskDescription.length > 0}
	<div class="description">
		{task.taskDescription}
	</div>
{/if}

<DetailsContainer>
	<DetailsContainerEntry icon="bi-clock" value={task.dateAdded} />
	<DetailsContainerEntry icon="bi-tag-fill" value={task.taskCategory} />
	<DetailsContainerEntry icon="bi-tags-fill" value={task.taskSubCategory} />
</DetailsContainer>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={() => onEditTask(task)}>
		<Icon name="pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button color="success" class="flex-fill" on:click={() => onCompleteTask(task)}>
		<Icon name="clipboard-check" />
		<span class="d-none d-sm-inline">Complete</span>
	</Button>
</div>

<style>
	.description {
		border: 1px solid #3f3f3f;
		background-color: #161616;
		border-radius: 0.5em;
		padding: 0.5em;
	}
</style>

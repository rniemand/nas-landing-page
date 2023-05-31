<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../nlp-api';
	export let link: UserLinkDto;

	const handleClick = () => {
		new UserLinksClient().recordLinkFollow(link.linkID).then((success: boolean) => {
			if(success) link.followCount += 1;
		});
		window.open(link.linkUrl, '_blank');
	};
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="link" on:click={handleClick}>
	<div class="title">{link.linkName}</div>
	<div class="img">
		<img src={`images/link/${link.linkImage}`} alt={link.linkName} />
	</div>
	<div class="info">
		<span class="id"># {link.linkID}</span>
		<span class="follow-count">{link.followCount}</span>
		<span class="order">{link.linkOrder}</span>
	</div>
</div>

<style>
	.link {
		width: 150px;
		border: 1px solid #585858;
		margin-right: 12px;
		padding: 3px;
		border-radius: 4px;
		box-shadow: 2px 2px 3px 0px #323232;
		background-color: #323232;
		color: #fff;
		cursor: pointer;
		margin-bottom: 12px;
	}
	.link:hover {
		background-color: #2e373a;
	}
	.link:last-child {
		margin-right: 0;
	}
	.title {
		text-align: center;
		font-weight: bold;
		margin-bottom: 6px;
		text-overflow: ellipsis;
		white-space: nowrap;
		overflow: hidden;
	}
	.img {
		text-align: center;
		margin-top: 2px;
		height: 85px;
	}
	.link img {
		max-width: 70px;
		height: auto;
		max-height: 70px;
		width: auto;
		border: 1px solid #000;
		padding: 3px;
		border-radius: 4px;
		background-color: #ececec;
	}
	.info {
		display: flex;
	}
	.info span {
		flex: auto;
		text-align: center;
	}
	.info .follow-count {
		color: #26ff26;
	}
	.info .order {
		color: #8c8c8d;
	}
</style>
